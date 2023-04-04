using DataAccess.Context;
using EATS365_Library.DTO;
using EATS365_Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        private EATS365Context _context;
        private static AccountDAO _instance = null;
        private static readonly object _instanceLock = new object();

        public static AccountDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountDAO();
                    }
                    return _instance;
                }
            }
        }

        public AccountDAO() => _context = new EATS365Context();

        public AccountDTO Login(string email, string password)
        {
            Account account = null;
            try
            {
                account = _context.Accounts.Where(a => a.AccountEmail.ToLower().Equals(email.ToLower()) && a.AccountPassword.Equals(password) 
                && a.AccountStatus.Equals("ACTIVED")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            _context.Entry(account).State = EntityState.Detached;

            if (account != null)
            {
                AccountDTO accountDTO = new AccountDTO
                {
                    AccountId = account.AccountId,
                    AccountEmail = account.AccountEmail,
                    AccountName = account.AccountName,
                    AccountPhone = account.AccountPhone,
                    AccountAddress = account.AccountAddress,
                    AccountBirthDay = account.AccountBirthDay,
                    AccountStartDate = account.AccountStartDate,
                    AccountStatus = account.AccountStatus
                };

                return accountDTO;
            }

            return null;
        }
    }
}
