using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core.Enums;
using MediaStudioService.ModelBulder;
using MediaStudioService.Models.Input;
using MediaStudioService.Models.Output;
using MediaStudioService.Services.audit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.AccountServic
{
    public class AccountService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditAccountService audit;
        public object AccountManager { get; private set; }
        public AccountService(MediaStudioContext context, AuditAccountService auditAccount)
        {
            audit = auditAccount;
            postgres = context;
        }

        public async Task<List<TypeAccount>> GetAccontTypes()
        {
            return await postgres.TypeAccount.ToListAsync();
        }

        public string UpdateTypeAccount(InputAccount inputAccount, string executorLogin)
        {
            var login = inputAccount.Login;
            var idTypeAccount = inputAccount.IdTypeAccount;

            CheckValidTypeAccount(idTypeAccount);
            CheckLoginExist(login);

            var account = postgres.Account.Include(i => i.IdTypeAccountNavigation)
                .Where(a => a.Login == login).FirstOrDefault();
            var newNameType = postgres.TypeAccount.Where(t => t.IdTypeAccount == idTypeAccount).FirstOrDefault().NameType;
            var oldNameType = account.IdTypeAccountNavigation.NameType;

            audit.Add(LogOperaion.Изменение, account.Login, executorLogin, oldValue: $"{oldNameType} => {newNameType}");

            account.IdTypeAccount = idTypeAccount;
            postgres.SaveChanges();

            audit.MarkSucces();
            return "Тип аккаунта успешно изменен!";
        }

        public string TryCreateAdminAccount(InputAccount inputAccount, string executorLogin)
        {
            audit.Add(LogOperaion.Добавление, inputAccount.Login, executorLogin);

            var login = inputAccount.Login;
            CheckValidTypeAccount(inputAccount.IdTypeAccount);
            if (LoginExists(login))
                throw new MyBadRequestException($"Пользователь с логином {login} уже существует!");

            var newAccount = AccountBuilderSerivice.Create(inputAccount);
            postgres.Account.Add(newAccount);
            postgres.SaveChanges();
            audit.MarkSucces(newAccount.IdAccount);
            return $"Пользователь {login} успешно создан!";
        }

        public int CreateAccount(InputAccount inputAccount)
        {
            CheckValidTypeAccount(inputAccount.IdTypeAccount);
            if (LoginExists(inputAccount.Login))
                throw new MyBadRequestException($"Пользователь с логином {inputAccount.Login} уже существует!");

            var newAccount = AccountBuilderSerivice.Create(inputAccount);
            postgres.Account.Add(newAccount);
            postgres.SaveChanges();
            return newAccount.IdAccount;
        }

        public object GetTokenStatus(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var name = jwtToken.Claims.First(cl => cl.Type == "unique_name").Value;
            var role = jwtToken.Claims.First(cl => cl.Type == "role").Value;
            return new { name, role };
        }

        public async Task<List<PageAccount>> GetAccounByType(int? idTypeAccount)
        {
            CheckValidTypeAccount(idTypeAccount.GetValueOrDefault());
            return await GetAllAsync()
                    .Where(pageAccount => pageAccount.IdTypeAccount == idTypeAccount)
                    .ToListAsync();
        }

        public Account GetAccountByLogin(string login)
        {
            if (postgres.Account.Any(account => account.Login == login))
            {
                throw new MyNotFoundException($"Аккаунт c логином {login} отсуствует в БД!");
            }

            return postgres.Account
                .AsNoTracking()
                .Where(account => account.Login == login)
                .Include(account => account.User)
                .FirstOrDefault();
        }

        public string DeleteAccount(string login, string executorLogin)
        {
            CheckLoginExist(login);

            audit.Add(LogOperaion.Удаление, login, executorLogin);

            var account = postgres.Account
                .Where(a => a.Login == login)
                .FirstOrDefault();

            postgres.Account.Remove(account);
            postgres.SaveChanges();
            audit.MarkSucces();

            return $"Пользователь {login} успешно удален!";
        }

        public IQueryable<PageAccount> GetAllAsync()
        {
            return postgres.Account.AsNoTracking()
                .Select(s => new PageAccount
                {
                    Login = s.Login,
                    IdTypeAccount = s.IdTypeAccount,
                    TypeAccount = s.IdTypeAccountNavigation.NameType
                }).AsQueryable();
        }

        private void CheckLoginExist(string login)
        {
            if (!LoginExists(login))
                throw new MyNotFoundException($"Пользователь с логином {login} отсуствует!");
        }

        private void CheckValidTypeAccount(int idTypeAccount)
        {
            if (!postgres.TypeAccount.Any(e => e.IdTypeAccount == idTypeAccount))
                throw new MyNotFoundException($"Ошибка! Тип учетной записи с idTypeAccount {idTypeAccount} отсуствует в БД!");
        }

        public bool AccountExists(InputAccount inputAccount)
        {
            var login = inputAccount.Login;
            var password = inputAccount.Password;
            return postgres.Account.Any(a => a.Login == login && a.Password == password);
        }

        public async Task<TypeAccount> GetTypeAccount(string username)
        {
            var idTypeAcount = postgres.Account.Where(a => a.Login == username).FirstOrDefault();
            return await postgres.TypeAccount.FindAsync(idTypeAcount.IdTypeAccount);
        }

        private bool LoginExists(string username)
        {
            return postgres.Account.Any(a => a.Login == username);
        }
    }
}
