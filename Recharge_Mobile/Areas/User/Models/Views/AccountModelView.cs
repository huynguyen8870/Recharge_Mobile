using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class AccountModelView
    {
        public int CustomerId { get; set; }
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public AccountModelView()
        {
         
        }

        public AccountModelView(int customerId, int phoneNumber, string password, string firstName, string lastName, string email, string address, string status)
        {
            CustomerId = customerId;
            PhoneNumber = phoneNumber;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Status = status;
        }


    }
}