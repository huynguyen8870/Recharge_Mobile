using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Models.Views
{
    public class AccountInfoVM
    {
        public AccountInfoVM()
        {
                
        }

        public AccountInfoVM(int id, string username, string phoneNumber, string firstName, string lastName, string role)
        {
            this.id = id;
            Username = username;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public int id;
        public string Username;
        public string PhoneNumber;
        public string FirstName;
        public string LastName;
        public string Role;
    }
}