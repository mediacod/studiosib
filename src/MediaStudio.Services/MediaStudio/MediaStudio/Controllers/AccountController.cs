namespace MediaStudio.Controllers
{
    using MediaStudioService.AccountServic;
    using MediaStudioService.Models.Input;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        // POST api/<AccountController>
        [HttpPost]
        public int Post(InputAccount inputAccount)
        {
            return _accountService.CreateAccount(inputAccount);
        }
    }
}
