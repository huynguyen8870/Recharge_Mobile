using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminChangePasswordVM
    {
        public AdminChangePasswordVM()
        {
                
        }

        public AdminChangePasswordVM(string oldPassword, string password, string confirmPassword)
        {
            OldPassword = oldPassword;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "password must have 8-50 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter old password!")]
        public string OldPassword { get; set; }
        [StringLength(50, MinimumLength = 8, ErrorMessage = "password must have 8-50 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter new password!")]
        public string Password { get; set; }
        [StringLength(50, MinimumLength = 8, ErrorMessage = "password must have 8-50 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter confirm password!")]
        public string ConfirmPassword { get; set; }

    }
}