﻿namespace PasswordManager.ViewModels
{
    public class GeneratePasswordViewModel
    {
        public bool IsConfigured { get; set; }
        public int PasswordLength { get; set; }
        public bool HasLowercase { get; set; }
        public bool HasUppercase { get; set; }
        public bool HasDigit { get; set; }
        public bool HasNonAlphanumeric { get; set; }
        public string GeneratedPassword { get; set; }
    }
}