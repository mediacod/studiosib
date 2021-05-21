namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.AccountServic;
    using MediaStudioService.Core;
    using MediaStudioService.Models.Input;
    using MediaStudioService.Models.Output;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("admin/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;

        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpPost("SignUp")]
        public string SignUp(InputAccount inputAccount)
        {
            //по умолчанию ставим роль "Пользователь"
            inputAccount.IdTypeAccount = 4;

            return accountService.TryCreateAdminAccount(inputAccount, User.Identity.Name);
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpPost("SignUpWithRole")]
        public string SignUpWithRole(InputAccount inputAccount)
        {
            return accountService.TryCreateAdminAccount(inputAccount, User.Identity.Name);
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpPost("UpdateType")]
        public string UpdateTypeAccount(InputAccount inputAccount)
        {
            return accountService.UpdateTypeAccount(inputAccount, User.Identity.Name);
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpGet("Types")]
        public async Task<List<TypeAccount>> GetAccountTypes()
        {
            return await accountService.GetAccontTypes();
        }

        [HttpGet("Status")]
        public object GetStatus(string token)
        {
            return accountService.GetTokenStatus(token);
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpGet("List")]
        public async Task<IEnumerable<PageAccount>> GetAccountList()
        {
            return await accountService.GetAllAsync()
            .ToListAsync();
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpGet("ByType")]
        public async Task<List<PageAccount>> GetAccounByType(int idTypeAccount)
        {
            return await accountService.GetAccounByType(idTypeAccount);
        }

        [Authorize(Policy = Policy.SignUpWithRole)]
        [HttpDelete("Delete")]
        public string DeleteAccount(string login)
        {
            return accountService.DeleteAccount(login, User.Identity.Name);
        }
    }
}