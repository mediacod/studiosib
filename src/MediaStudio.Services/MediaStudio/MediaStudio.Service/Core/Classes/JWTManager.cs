using DBContext.Models;
using MediaStudio.Core.Enums;
using MediaStudioService.Core.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MediaStudio.Core
{
    public class JWTManager
    {
        public const string ISSUER = "MediaStudioServer"; // издатель токена
        public const string AUDIENCE = "MediaStudioClient"; // потребитель токена

        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public double LifeTime { get; private set; }

        private readonly IConfiguration configuration;
        private readonly ClaimManager claimManager;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        private static readonly int[] adminAccountTypes = new[]
        {
            (int)AccountTypes.Разработчик,  (int)AccountTypes.Администратор,
            (int)AccountTypes.Модератор, (int)AccountTypes.Провайдер,
        };

        public JWTManager(IConfiguration Configuration, Task<TypeAccount> typeAccount, string login)
        {
            configuration = Configuration;
            claimManager = new ClaimManager();
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            BuldAccessToken(typeAccount, login);
            GenerateRefreshToken();
            LoadLifeTime(typeAccount);
        }

        public static JwtBearerOptions BuldJWTOption(IConfiguration Configuration, JwtBearerOptions jwtBearerOptions)
        {
            // configure jwt authentication
            var JWT_KEY = GetJWTKey(Configuration);
            var key = Encoding.ASCII.GetBytes(JWT_KEY);

            jwtBearerOptions.RequireHttpsMetadata = false;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                // будет ли валидироваться время существования
                ValidateLifetime = true,

                // валидация ключа безопасности
                ValidateIssuerSigningKey = true,

                // установка ключа безопасности
                IssuerSigningKey = new SymmetricSecurityKey(key),

                // укзывает, будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,
                ValidIssuer = ISSUER,

                // будет ли валидироваться потребитель токена
                ValidateAudience = true,
                ValidAudience = AUDIENCE
            };
            return jwtBearerOptions;
        }

        private void BuldAccessToken(Task<TypeAccount> typeAccount, string username)
        {
            // создаем JWT-токен
            var secretKey = GetJWTKey(configuration);
            var lifetime = GetLifeTimeAccess(configuration, typeAccount.Result);
            var byteKey = Encoding.ASCII.GetBytes(secretKey);
            var claimsIdentity = claimManager.BuldClaimsIdentity(username, typeAccount);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = ISSUER,
                Audience = AUDIENCE,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var securitToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            AccessToken = jwtSecurityTokenHandler.WriteToken(securitToken);
        }

        private void GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            RefreshToken = Convert.ToBase64String(randomNumber);
        }

        public void LoadLifeTime(Task<TypeAccount> typeAccount)
        {
            var sectionName = adminAccountTypes.Contains(typeAccount.Result.IdTypeAccount)
                ? "LIFETIME_ADMIN_DAYS"
                : "LIFETIME_USER_DAYS";

            var lifeTime = configuration.GetSection("JWT_REFRESH").GetSection(sectionName).Value;
            LifeTime = double.Parse(lifeTime);
        }
        private static string GetJWTKey(IConfiguration Configuration)
        {
            return Configuration.GetSection("JWT_ACCESS").GetSection("SECRET_KEY").Value;
        }

        private static double GetLifeTimeAccess(IConfiguration Configuration, TypeAccount typeAccount)
        {
            var sectionName = adminAccountTypes.Contains(typeAccount.IdTypeAccount)
                ? "LIFETIME_ADMIN_MINUTES"
                : "LIFETIME_USER_MINUTES";

            var lifeTime = Configuration.GetSection("JWT_ACCESS").GetSection(sectionName).Value;
            return double.Parse(lifeTime);
        }

    }
}
