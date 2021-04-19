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

        //public AccountModelView Login(string phonenumber, string password)
        //{
        //    entities = new RechargeMobileEntities();
        //    //var accountInfo = entities.Customers.Select(d => new AccountModelView() 
        //    //{
        //    //    CustomerId = d.CustomerId,
        //    //    PhoneNumber = d.PhoneNumber,
        //    //    FirstName = d.FirstName,
        //    //    LastName = d.LastName
        //    //}).Where(d => PhoneNumber.Equals(phonenumber) && d.Acc.Equals(password)).FirstOrDefault();
        //    var account = entities.Customers.Where(d => d.PhoneNumber.Equals(phonenumber) && d.Password.Equals(password)).FirstOrDefault();
        //    if(account != null)
        //    {
        //        AccountModelView accountInfo = new AccountModelView()
        //        {
        //            CustomerId = account.CustomerId,
        //            PhoneNumber = account.PhoneNumber,
        //            FirstName = account.FirstName,
        //            LastName = account.LastName
        //        };
        //        return accountInfo;
        //    } else
        //    {
        //        AccountModelView accountInfo = null;
        //        return accountInfo;
        //    }
        //}

        public Customer Login(string phonenumber, string password)
        {
            entities = new RechargeMobileEntities();
            var account = entities.Customers.Where(d => d.PhoneNumber.Equals(phonenumber) && d.Password.Equals(password)).FirstOrDefault();
            if(account != null)
            {
                account.Password = "";
            }           
            return account;
        }

        public Admin LoginAdmin(string username, string password)
        {
            entities = new RechargeMobileEntities();
            var account = entities.Admins.Where(d => d.Username.ToLower().Equals(username.ToLower()) && d.Password.Equals(password)).FirstOrDefault();
            if (account != null)
            {
                account.Password = "";
            }
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
                Lock = "Off",
                Status = "Active"
            };
            entities.Customers.Add(customer);
            entities.SaveChanges();
        }

        public void CustomerTransactionOnRegister(string phone)
        {
            entities = new RechargeMobileEntities();
            CustomerRecharge item = entities.CustomerRecharges.Where(d => d.PhoneNumber == phone).FirstOrDefault();
            if (item == null)
            {
                DateTime thisTime = DateTime.Now;
                CustomerRecharge newItem = new CustomerRecharge
                {
                    PhoneNumber = phone,
                    TimeRemain = 0,
                    RegularTime = thisTime,
                    SpecialTime = thisTime,
                    DebitAmount = 0,
                    TimeToPay = thisTime,
                    LinkAccount = "Yes"
                };
            }
        }

    }
}