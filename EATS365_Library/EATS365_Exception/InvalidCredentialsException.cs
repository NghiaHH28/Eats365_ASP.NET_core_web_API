using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.EATS365_Exception
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Incorrect password!")
        {
        }

        public InvalidCredentialsException(string message)
            : base(message)
        {
        }

        public InvalidCredentialsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
