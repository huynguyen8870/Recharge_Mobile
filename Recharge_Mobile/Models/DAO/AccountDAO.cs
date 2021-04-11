using Recharge_Mobile.Areas.User.Models.Views;
using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Models.DAO
{
    public class AccountDAO
    {
        static RechargeMobileEntities entities;

        public AccountModelView Login(string phonenumber, string password)
        {
            entities = new RechargeMobileEntities();
            var accountInfo = entities.Customers.Where(d => d.PhoneNumber == phonenumber && d.Password == password).FirstOrDefault();
            AccountModelView account = new AccountModelView()
            {
                CustomerId = accountInfo.CustomerId,
                PhoneNumber = accountInfo.PhoneNumber,
                FirstName = accountInfo.FirstName,
                LastName = accountInfo.LastName
            };
            return account;
        }

        public void Register(string firstname, string lastname, string email, string phonenumber, string password)
        {
            entities = new RechargeMobileEntities();
            Customer customer = new Customer()
            {
                PhoneNumber = phonenumber,
                Password = password,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Busy = "Off",
                Status = "Active"
            };
            entities.Customers.Add(customer);
            entities.SaveChanges();
        }
    }
}