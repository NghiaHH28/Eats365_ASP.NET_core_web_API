using DataAccess.Context;
using EATS365_Library.DTO;
using EATS365_Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return null;

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

        public string GenerageToken(AccountDTO accountDTO)
        {
            if (accountDTO == null) return null;

            string role = null;

            if (accountDTO.AccountId.StartsWith("AD")) role = "admin";
            if (accountDTO.AccountId.StartsWith("US")) role = "user";
            if (accountDTO.AccountId.StartsWith("CH")) role = "chef";
            if (accountDTO.AccountId.StartsWith("SP")) role = "shipper";

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(GetSecretKey());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, accountDTO.AccountName),
                    new Claim(ClaimTypes.Email, accountDTO.AccountEmail),
                    new Claim("ID", accountDTO.AccountId),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("TokenID", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);

        }

        private string GetSecretKey()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            var str = config["AppSettings:SecretKey"];
            return str;
        }
    }
}
