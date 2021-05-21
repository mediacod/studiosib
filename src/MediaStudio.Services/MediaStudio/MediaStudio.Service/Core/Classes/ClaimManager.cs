﻿using DBContext.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MediaStudioService.Core.Classes
{
    public class ClaimManager
    {
        public ClaimsIdentity BuldClaimsIdentity(string username, Task<TypeAccount> typeAccount)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, typeAccount.Result.NameType)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity
                (claims, "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
