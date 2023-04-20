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
        public string GenerateToken(AccountDTO accountDTO) => AccountDAO.Instance.GenerateToken(accountDTO);

        public async Task<AccountDTO> LoginAsync(string email, string password) => await AccountDAO.Instance.LoginAsync(email, password);
    }
}
