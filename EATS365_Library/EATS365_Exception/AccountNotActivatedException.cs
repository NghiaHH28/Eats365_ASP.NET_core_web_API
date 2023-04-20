using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.EATS365_Exception
{
    public class AccountNotActivatedException : Exception
    {
        public AccountNotActivatedException() : base("The account has not been activated!")
        {
        }

        public AccountNotActivatedException(string message)
            : base(message)
        {
        }

        public AccountNotActivatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
