using System;
using System.Text;

namespace PasswordManager
{
    public class Generator
    {
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digit = "0123456789";
        private const string NonAlphanumeric = "!@#$%^&*()_-+=[{]};:<>|./?";

        public string GeneratePassword(int length, bool hasLowercase, bool hasUppercase, bool hasDigit,
            bool hasNonAlphanumeric)
        {
            var valid = new StringBuilder();
            if (hasLowercase) valid.Append(Lowercase);
            if (hasUppercase) valid.Append(Uppercase);
            if (hasDigit) valid.Append(Digit);
            if (hasNonAlphanumeric) valid.Append(NonAlphanumeric);
            
            var result = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                result.Append(valid[rnd.Next(valid.Length)]);
            }
            
            return result.ToString();
        }
    }
}