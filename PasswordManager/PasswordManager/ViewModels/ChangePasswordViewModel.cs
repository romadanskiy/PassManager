﻿using System.ComponentModel.DataAnnotations;

namespace PasswordManager.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}