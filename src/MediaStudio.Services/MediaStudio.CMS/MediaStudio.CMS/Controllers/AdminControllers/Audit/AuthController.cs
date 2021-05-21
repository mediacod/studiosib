﻿namespace MediaStudio.Controllers
{
    using MediaStudioService.Core;
    using MediaStudioService.Models.Input;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("SignIn")]
        public object SignIn(InputAccount inputAccount)
        {
            return authService.GetToken(inputAccount, Request.HttpContext);
        }

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public object Refresh([FromBody] JObject jsonValue)
        {
            var token = jsonValue["accessToken"].ToString();
            var refreshToken = jsonValue["refreshToken"].ToString();

            return authService.RefreshToken(token, refreshToken, Request.HttpContext);
        }

        [HttpPost("ForceLogout")]
        public string ForceLogout(string login)
        {
            return "Success";
        }

        [Authorize(Policy = Policy.AdminApplication)]
        [HttpGet("CheckAccessAdmin")]
        public string CheckAccessAdmin()
        {
            return "Success";
        }
    }
}