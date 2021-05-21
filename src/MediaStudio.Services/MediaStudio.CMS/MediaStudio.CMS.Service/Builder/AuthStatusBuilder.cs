using DBContext.Models;
using MediaStudio.Core;
using System;

namespace MediaStudioService.ModelBulder
{
    public static class AuthStatusBuilder
    {
        public static AuthStatus BuldAuthStatus(ClientInformation clientInfo, JWTManager tokenManager)
        {
            var now = DateTime.Now;
            return new AuthStatus
            {
                Ipv4 = clientInfo.IPv4,
                Jwt = tokenManager.AccessToken,
                Login = clientInfo.Login,
                NameDevice = clientInfo.NameDevice,
                Refresh = tokenManager.RefreshToken,
                UserAgent = clientInfo.UserAgent,
                ValidFrom = now,
                ValidUntil = now.AddDays(tokenManager.LifeTime),
            };
        }

        public static AuthStatus GetUpdatedAuth(AuthStatus oldAuth, JWTManager tokenManager)
        {
            var now = DateTime.Now;

            oldAuth.Jwt = tokenManager.AccessToken;
            oldAuth.Refresh = tokenManager.RefreshToken;
            oldAuth.ValidFrom = now;
            oldAuth.ValidUntil = now.AddDays(tokenManager.LifeTime);

            return oldAuth;
        }
    }
}
