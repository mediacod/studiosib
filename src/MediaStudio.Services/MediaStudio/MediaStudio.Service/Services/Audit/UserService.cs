using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Models.Input;
using MediaStudioService.AccountServic;
using Microsoft.EntityFrameworkCore;
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
        public int PostUser(InputUser user)
        {
            CheckValidUser(user);
            postgres.User.Add(
                new User { 
                    Gender =user.Gender,
                    DateBirthday = user.DateBirthday,
                    FirstName =user.FirstName,
                    IdAccount = user.IdAccount,
                    IdCloudPath = user.IdCloudPath,
                    IdUser = user.IdUser,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    PhoneNumber = user.PhoneNumber});

            postgres.SaveChanges();
            return user.IdUser;
        }

        public User UpdateUser(User newUser, string login)
        {
            CheckValidUser(newUser);

            var account = _accountService.GetAccountByLogin(login);
            if (account.User.IdUser != newUser.IdUser)
            {
                throw new MyBadRequestException($"Обновляемый идентификатор user не принадлежит  пользователю текущего логину из токена {login}!");
            }
            account.User = newUser;
            postgres.SaveChanges();
            return newUser;
        }

        public User GetUser(string login)
        {
            var account = _accountService.GetAccountByLogin(login);
            return account.User;
        }

        public void CheckValidUser(InputUser user)
        {
            if(postgres.User.Any(user => user.IdAccount == user.IdAccount))
            {
                throw new MyBadRequestException($"User c IdAccount {user.IdAccount} уже существует!");
            }

            CheckValidAccount(user.IdAccount);
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
