using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Hash
{
    public class GenerageID
    {
        public string GenerateNewID(string prefix, string lastAccountId)
        {
            string newId = "";
            try
            {
                // Remove prefix from the last employee ID
                int idNumber = int.Parse(lastAccountId.Substring(prefix.Length));
                // Increment the ID number
                idNumber++;
                // Format the new ID with the prefix and the incremented ID number
                newId = string.Format("{0}{1:D4}", prefix, idNumber);
            }
            catch (FormatException e)
            {
                // If the last employee ID is not in the correct format, return an error message
                return "Invalid employee ID";
            }
            return newId;
        }

        public string GetPrefixFromProductName(string productName)
        {
            // Get the first letter of each word in the product name and concatenate them
            string[] words = productName.Split(' ');
            string prefix = "";
            foreach (string word in words)
            {
                prefix += word[0];
            }
            // Convert the prefix to uppercase and return it
            return prefix.Substring(0, 2).ToUpper();
        }
    }
}
