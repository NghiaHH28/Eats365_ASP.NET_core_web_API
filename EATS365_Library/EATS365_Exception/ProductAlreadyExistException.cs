using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EATS365_Library.EATS365_Exception
{
    
    public class ProductAlreadyExistException : Exception
    {
        public ProductAlreadyExistException() : base("The product is already exist!")
        {
        }

        public ProductAlreadyExistException(string message)
            : base(message)
        {
        }

        public ProductAlreadyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
