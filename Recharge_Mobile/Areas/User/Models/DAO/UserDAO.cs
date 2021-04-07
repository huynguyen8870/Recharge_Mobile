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

        public Views.AccountModelView AccountSetting(int Id)
        {
            entities = new RechargeMobileEntities();
            var Account = entities.Customers.Where(d => d.CustomerId == Id).FirstOrDefault();
            AccountModelView accountInfo = new AccountModelView()
            {
                CustomerId = Account.CustomerId,
                PhoneNumber = Account.PhoneNumber,
                Password = Account.Password,
                FirstName = Account.FirstName,
                LastName = Account.LastName,
                Email = Account.Email,
                Address = Account.Address,
                Busy = Account.Busy,
                Lock = Account.Lock,
                Status = Account.Status
            };
            return accountInfo;
        }

        public List<TransactionModelView> TransactionList(string phonenumber)
        {
            entities = new RechargeMobileEntities();
            var TransListRaw = entities.Transactions.Where(d => d.PhoneNumber == phonenumber).ToList();
            var TransList = TransListRaw.Select(d => new TransactionModelView()
            {
                TransactionId = d.TransactionId,
                PhoneNumber = d.PhoneNumber,
                PaymentMethod = d.PaymentMethod,
                RRecharge = d.RRechargeId ?? 0,
                SRecharge = d.SRechargeId ?? 0,
                DateTime = d.DateTime,
                Status = d.Status
            }).ToList();
            return TransList;
        }
    }
}