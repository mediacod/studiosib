namespace MediaStudio.Core
{
    using System.Collections.Generic;
    using MediaStudioService.Core;
    using Microsoft.AspNetCore.Authorization;

    public class PolicyManager
    {
        private static readonly Dictionary<string, string[]> roleToPolicy = new Dictionary<string, string[]>()
        {
            { Policy.SignUpWithRole, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор } },
            { Policy.FullAlbumControl, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор } },
            { Policy.CreateTrack, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор}  },
            { Policy.AdminApplication, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор} },
            { Policy.FullProperties, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор}  },
            { Policy.FullPerformer, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор}  },
            { Policy.AuditViewing, new string[] {Roles.Администратор, Roles.Разработчик, Roles.Модератор} },
            { Policy.FullPlaylist, new string[] {Roles.Администратор, Roles.Разработчик , Roles.Модератор}  },
            { Policy.FullPage, new string[] {Roles.Администратор, Roles.Разработчик ,}  },
        };

        public static AuthorizationOptions BuldAuthOption(string namePolicy, AuthorizationOptions authOption)
        {
            var roles = roleToPolicy[namePolicy];
            authOption.AddPolicy(namePolicy, policy => policy.RequireRole(roles));
            return authOption;
        }
    }
}