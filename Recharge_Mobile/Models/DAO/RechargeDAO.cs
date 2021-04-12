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
    }
}