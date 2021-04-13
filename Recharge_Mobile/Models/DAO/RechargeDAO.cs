using Recharge_Mobile.Areas.User.Models.Views;
using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.Recharge.Models.DAO
{
    public class RechargeDAO
    {
        static RechargeMobileEntities entities;

        public List<RRechargeModelView> RRechargeList()
        {
            entities = new RechargeMobileEntities();
            var RRListRaw = entities.RegularRecharges.Where(d => d.Status == "Active").OrderBy(d => d.Price).ToList();
            var RRList = RRListRaw.Select(d => new RRechargeModelView()
            {
                RRechargeId = d.RRechargeId,
                RRName = d.RRName,
                BaseTime = Convert.ToInt64(d.BaseTime) / 60,
                BonusTime = Convert.ToInt64(d.BonusTime),
                TotalTime = Convert.ToInt64(d.TotalTime),
                Price = d.Price,
                Duration = Convert.ToInt64(d.Duration),
                Description = d.Description,
            }).ToList();
            return RRList;
        }

        public List<SRechargeModelView> SRechargeList()
        {
            entities = new RechargeMobileEntities();
            var SRListRaw = entities.SpecialRecharges.Where(d => d.Status == "Active").ToList();
            var SRList = SRListRaw.Select(d => new SRechargeModelView()
            {
                SRechargeId = d.SRechargeId,
                SRName = d.SRName,
                Duration = Convert.ToInt64(d.Duration),
                Price = d.Price,
                Description = d.Description,
            }).ToList();
            return SRList;
        }

        public RRechargeModelView RRechargeById(int id)
        {
            entities = new RechargeMobileEntities();
            var RRechargeRaw = entities.RegularRecharges.Where(d => d.RRechargeId == id).FirstOrDefault();
            RRechargeModelView RRecharge = new RRechargeModelView()
            {
                RRName = RRechargeRaw.RRName,
                BaseTime = Convert.ToInt64(RRechargeRaw.BaseTime),
                BonusTime = Convert.ToInt64(RRechargeRaw.BonusTime),
                TotalTime = Convert.ToInt64(RRechargeRaw.TotalTime),
                Price = RRechargeRaw.Price,
                Duration = Convert.ToInt64(RRechargeRaw.Duration)
            };
            return RRecharge;
        }

        public SRechargeModelView SRechargeById(int id)
        {
            entities = new RechargeMobileEntities();
            var SRechargeRaw = entities.SpecialRecharges.Where(d => d.SRechargeId == id).FirstOrDefault();
            SRechargeModelView SRecharge = new SRechargeModelView()
            {
                SRName = SRechargeRaw.SRName,
                Duration = Convert.ToInt64(SRechargeRaw.Duration),
                Price = SRechargeRaw.Price,
            };
            return SRecharge;
        }

        public void CheckoutComplete(TransactionModelView transaction)
        {
            entities = new RechargeMobileEntities();
            Transaction transactionEntity = new Transaction()
            {
                PhoneNumber = transaction.PhoneNumber,
                PaymentMethod = transaction.PaymentMethod,
                RRechargeId = transaction.RRechargeId,
                SRechargeId = transaction.SRechargeId,
                DateTime = transaction.DateTime,
                Status = transaction.Status
            };
            entities.Transactions.Add(transactionEntity);
            entities.SaveChanges();
        }

        public TransactionModelView TransactionById(int id)
        {
            string rechargename;
            decimal price;

            entities = new RechargeMobileEntities();
            var trasactionRaw = entities.Transactions.Where(d => d.TransactionId == id).FirstOrDefault();
            
            if(trasactionRaw.RRechargeId > 0)
            {
                var recharge = entities.RegularRecharges.Where(d => d.RRechargeId == trasactionRaw.RRechargeId).FirstOrDefault();
                rechargename = recharge.RRName;
                price = recharge.Price;
            } else
            {
                var recharge = entities.SpecialRecharges.Where(d => d.SRechargeId == trasactionRaw.SRechargeId).FirstOrDefault();
                rechargename = recharge.SRName;
                price = recharge.Price;
            }

            TransactionModelView transaction = new TransactionModelView()
            {
                TransactionId = trasactionRaw.TransactionId,
                PhoneNumber = trasactionRaw.PhoneNumber,
                PaymentMethod = trasactionRaw.PaymentMethod,
                RRechargeId = trasactionRaw.RRechargeId,
                SRechargeId = trasactionRaw.SRechargeId,
                RechargeName = rechargename,
                Price = price,
                DateTime = trasactionRaw.DateTime,
                Status = trasactionRaw.Status,
            };
            return transaction;
        }

        public void PaidTransaction(int id)
        {
            entities = new RechargeMobileEntities();
            var transaction = entities.Transactions.Where(d => d.TransactionId == id).FirstOrDefault();
            transaction.Status = "Paid";
            entities.SaveChanges();
        }
    }
}