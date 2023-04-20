using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.EATS365_Exception
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("Account does not exist!")
        {
        }

        public AccountNotFoundException(string message)
            : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
