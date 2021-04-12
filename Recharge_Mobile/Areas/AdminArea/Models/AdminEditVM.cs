using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminEditVM
    {
        public AdminEditVM()
        {

        }

        public AdminEditVM(int adminId, string firstName, string lastName,
            string phoneNumber, string email, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }

        public int AdminId { get; set; }
        [StringLength(50, ErrorMessage = "max length = 50 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter first name!")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "max length = 50 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter last name!")]
        public string LastName { get; set; }
        [RegularExpression("([0-9]{10})", ErrorMessage = "phone number must have 10 numbers!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter phone number!")]
        public string PhoneNumber { get; set; }
        [StringLength(100, ErrorMessage = "max length = 100 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter email!")]
        public string Email { get; set; }
        [StringLength(500, ErrorMessage = "max length = 500 characters!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "please enter address!")]
        public string Address { get; set; }
    }
}