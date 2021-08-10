﻿using DBContext.Connect;
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
        public int PostUser(UserModel user)
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

        public void CheckValidUser(UserModel user)
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
