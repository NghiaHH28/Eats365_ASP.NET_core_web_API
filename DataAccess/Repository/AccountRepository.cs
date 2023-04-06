using DataAccess.DAO;
using EATS365_Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public string GenerageToken(AccountDTO accountDTO) => AccountDAO.Instance.GenerageToken(accountDTO);

        public AccountDTO Login(string email, string password) => AccountDAO.Instance.Login(email, password);
    }
}
