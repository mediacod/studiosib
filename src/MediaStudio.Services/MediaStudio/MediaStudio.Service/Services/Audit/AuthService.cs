using DBContext.Connect;
using MediaStudio.Core;
using MediaStudioService.AccountServic;
using MediaStudioService.Core.Enums;
using MediaStudioService.ModelBulder;
using MediaStudioService.Models.Input;
using MediaStudioService.Services.audit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace MediaStudioService.Services
{
    public class AuthService
    {
        private readonly MediaStudioContext postgres;
        private readonly IConfiguration Configuration;
        private readonly AccountService accountService;
        private readonly AuthHistoryService audit;
        private ClientInformation clientInfo;
        public object AccountManager { get; private set; }

        public AuthService(MediaStudioContext context, IConfiguration Configuration,
            AccountService accountService, AuthHistoryService authHistoryService)
        {
            postgres = context;
            this.Configuration = Configuration;
            this.accountService = accountService;
            audit = authHistoryService;
        }

        public object RefreshToken(string token, string refreshToken, HttpContext httpContext)
        {
            clientInfo = new ClientInformation(httpContext);
            audit.Add(LogOperaion.Обновление_токена, clientInfo);

            var authStatuses = postgres.AuthStatus.Where(a => a.Jwt == token && a.Refresh == refreshToken).ToList();

            if (!authStatuses.Any())
                throw new InvalidOperationException($"Невалидные токены!");
            var auth = authStatuses.FirstOrDefault();
            var login = auth.Login;
            audit.AddLogin(login);

            if (auth.ValidUntil <= DateTime.Now)
            {
                postgres.AuthStatus.Remove(authStatuses.FirstOrDefault());
                throw new InvalidOperationException("Срок действия токена истек!");
            }

            if (auth.Ipv4 != clientInfo.IPv4 && auth.UserAgent != clientInfo.UserAgent)
            {
                var loginAuthStatuses = postgres.AuthStatus.Where(a => a.Login == login).ToList();

                //ForceLogOut
                postgres.RemoveRange(loginAuthStatuses);
                postgres.SaveChanges();

                audit.MarkForseDeleteJWT(LogOperaion.Завершение_всех_сеансов);

                throw new InvalidOperationException("Остановитесь на путях своих!");
            }

            var typeAccount = accountService.GetTypeAccount(login);

            // JWTManager формирует access, refresh токены и время жизни
            JWTManager tokenManager = new JWTManager(Configuration, typeAccount, login);
            var updatedAuth = AuthStatusBuilder.GetUpdatedAuth(auth, tokenManager);

            postgres.AuthStatus.Update(updatedAuth);
            postgres.SaveChanges();

            audit.MarkSucces();
            return new { tokenManager.AccessToken, tokenManager.RefreshToken };
        }

        public object GetToken(InputAccount inputAccount, HttpContext httpContext)
        {
            var login = inputAccount.Login;
            // подгружаем данные клиента
            clientInfo = new ClientInformation(httpContext) { Login = login };

            // логируем
            audit.Add(LogOperaion.Авторизация, clientInfo);

            if (!accountService.AccountExists(inputAccount))
                throw new InvalidOperationException("Invalid username or password!");

            var typeAccount = accountService.GetTypeAccount(login);

            // JWTManager формирует access, refresh токены и время жизни
            JWTManager tokenManager = new JWTManager(Configuration, typeAccount, login);

            // строим модель для Postgres и сохраняем в БД
            var authStatus = AuthStatusBuilder.BuldAuthStatus(clientInfo, tokenManager);
            postgres.AuthStatus.Add(authStatus);
            postgres.SaveChanges();

            audit.MarkSucces();
            return new { tokenManager.AccessToken, tokenManager.RefreshToken };
        }
    }
}
