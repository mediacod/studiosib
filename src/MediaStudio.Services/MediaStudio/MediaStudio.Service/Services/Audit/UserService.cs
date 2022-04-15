using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Models.Input;
using MediaStudioService.AccountServic;
using System.Linq;

namespace MediaStudio.Service.Services.Audit
{
    public class UserService
    {
        private readonly MediaStudioContext postgres;
        private readonly AccountService _accountService;

        public UserService(MediaStudioContext context, AccountService accountService)
        {
            postgres = context;
            _accountService = accountService;
        }
        public int PostUser(UserModel userModel, string login)
        {
            CheckValidUser(userModel);
            var idAccount = _accountService.GetIdAccountByLogin(login);

            var user = new User
            {
                Gender = userModel.Gender,
                DateBirthday = userModel.DateBirthday,
                FirstName = userModel.FirstName,
                IdAccount = idAccount,
                IdCloudPath = userModel.IdCloudPath,
                LastName = userModel.LastName,
                Patronymic = userModel.Patronymic,
                PhoneNumber = userModel.PhoneNumber
            };
            postgres.User.Add(user);
            postgres.SaveChanges();
            return user.IdUser;
        }

        public UserUpdateEvent UpdateUser(UpdateUserModel user, string login)
        {
            var account = _accountService.GetAccountByLogin(login);
            account.User.Gender = user.Gender;
            account.User.DateBirthday = user.DateBirthday;
            account.User.LastName = user.LastName;
            account.User.Patronymic = user.Patronymic;
            account.User.PhoneNumber = user.PhoneNumber;       
            postgres.User.Update(account.User);
            postgres.SaveChanges();
            return new UserUpdateEvent();
        }

        public User GetUser(string login)
        {
            var account = _accountService.GetAccountByLogin(login);
            account.Password = null;
            return account.User;
        }

        public void CheckValidUser(UserModel newUser)
        {
            if(postgres.User.Any(user => user.IdAccount == newUser.IdAccount))
            {
                throw new MyBadRequestException($"User c IdAccount {newUser.IdAccount} уже существует!");
            }

            CheckValidAccount(newUser.IdAccount);
        }

        public void CheckValidIdUser(int idUser)
        {
            if (postgres.User.Any(user => user.IdUser == idUser))
            {
                throw new MyBadRequestException($"User c id {idUser} не существует!");
            }
        }

        public void CheckValidUser(User user)
        {
            if (postgres.User.Any(user => user.IdAccount == user.IdAccount))
            {
                throw new MyBadRequestException($"User c IdAccount {user.IdAccount} уже существует!");
            }

            CheckValidAccount(user.IdAccount);
        }

        public void CheckValidAccount(int IdAccount)
        {
            if (!postgres.Account.Any(account => account.IdAccount == IdAccount))
            {
                throw new MyNotFoundException($"Аккаунт {IdAccount} отсуствует в БД!");
            }
        }
    }
}
