using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class AccountModelView
    {
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Busy { get; set; }
        public string Lock { get; set; }
        public string Status { get; set; }


        public AccountModelView()
        {
         
        }

        public AccountModelView(int customerId, string phoneNumber, string password, string firstName, string lastName, string email, string address, string busy, string @lock, string status)
        {
            CustomerId = customerId;
            PhoneNumber = phoneNumber;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Busy = busy;
            Lock = @lock;
            Status = status;
        }
    }
}