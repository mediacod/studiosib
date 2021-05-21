using DBContext.Models;
using MediaStudioService.Models.Input;

namespace MediaStudioService.ModelBulder
{
    public class AccountBuilderSerivice
    {
        public static Account Create(InputAccount inputAccount)
        {
            var newAccount = new Account()
            {
                IdTypeAccount = inputAccount.IdTypeAccount,
                Login = inputAccount.Login,
                Password = inputAccount.Password,
            };

            return newAccount;
        }
    }
}
