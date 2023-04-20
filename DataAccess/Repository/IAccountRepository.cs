using EATS365_Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IAccountRepository
    {
        public Task<AccountDTO> LoginAsync(string email, string password);

        public string GenerateToken(AccountDTO accountDTO);
    }
}
