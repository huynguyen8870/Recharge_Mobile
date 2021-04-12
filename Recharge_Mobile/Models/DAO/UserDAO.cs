using Recharge_Mobile.Areas.User.Models.Views;
using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.DAO
{
    public class UserDAO
    {
        static RechargeMobileEntities entities;

        public CustomerRechargeModelView AccountDetail(string phonenumber)
        {
            entities = new RechargeMobileEntities();
            var Account = entities.CustomerRecharges.Where(d => d.PhoneNumber == phonenumber).FirstOrDefault();
            CustomerRechargeModelView customerRechargeModelView = new CustomerRechargeModelView()
            {
                PhoneNumber = Account.PhoneNumber,
                TimeRemain = Convert.ToInt64(Account.TimeRemain),
                RegularTime = Account.RegularTime,
                SpecialTime = Account.SpecialTime,
                DebitAmount = Account.DebitAmount,
                TimeToPay = Account.TimeToPay
            };
            return customerRechargeModelView;
        }

        public List<TransactionModelView> TransactionList(string phonenumber)
        {
            entities = new RechargeMobileEntities();
            var TransListRaw = entities.Transactions.Where(d => d.PhoneNumber == phonenumber && d.PaymentMethod != "Debit").ToList();
            var TransList = TransListRaw.Select(d => new TransactionModelView()
            {
                TransactionId = d.TransactionId,
                PhoneNumber = d.PhoneNumber,
                PaymentMethod = d.PaymentMethod,
                RRechargeId = d.RRechargeId ?? 0,
                SRechargeId = d.SRechargeId ?? 0,
                DateTime = d.DateTime,
                Status = d.Status
            }).ToList();
            return TransList;
        }

        public List<TransactionModelView> DebitTransactionList(string phonenumber)
        {
            entities = new RechargeMobileEntities();
            var DebitListRaw = entities.Transactions.Where(d => d.PhoneNumber == phonenumber && d.PaymentMethod == "Debit").ToList();
            var DebitList = DebitListRaw.Select(d => new TransactionModelView()
            {
                TransactionId = d.TransactionId,
                PhoneNumber = d.PhoneNumber,
                PaymentMethod = d.PaymentMethod,
                RRechargeId = d.RRechargeId ?? 0,
                SRechargeId = d.SRechargeId ?? 0,
                DateTime = d.DateTime,
                Status = d.Status
            }).ToList();
            return DebitList;
        }

        public AccountModelView AccountSetting(int Id)
        {
            entities = new RechargeMobileEntities();
            var Account = entities.Customers.Where(d => d.CustomerId == Id).FirstOrDefault();
            AccountModelView accountInfo = new AccountModelView()
            {
                CustomerId = Account.CustomerId,
                PhoneNumber = Account.PhoneNumber,
                FirstName = Account.FirstName,
                LastName = Account.LastName,
                Email = Account.Email,
                Address = Account.Address,
            };
            return accountInfo;
        }
        
        public void AccountChange(AccountModelView accountModelView)
        {
            entities = new RechargeMobileEntities();
            var Account = entities.Customers.Where(d => d.CustomerId == accountModelView.CustomerId).FirstOrDefault();
            Account.FirstName = accountModelView.FirstName;
            Account.LastName = accountModelView.LastName;
            Account.Email = accountModelView.Email;
            Account.Address = accountModelView.Address;
            entities.SaveChanges();
        }

        public string AccountPassword(string phonenumber)
        {
            entities = new RechargeMobileEntities();
            var Account = entities.Customers.Where(d => d.PhoneNumber == phonenumber).FirstOrDefault();
            var AccountPass = Account.Password;
            return AccountPass;
        }

        public void PasswordChange(string phonenumber ,string newpassword)
        {
            entities = new RechargeMobileEntities();
            var Account = entities.Customers.Where(d => d.PhoneNumber == phonenumber).FirstOrDefault();
            Account.Password = newpassword;
            entities.SaveChanges();
        }
    }
}