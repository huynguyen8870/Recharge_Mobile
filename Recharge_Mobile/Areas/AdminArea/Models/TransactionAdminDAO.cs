using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class TransactionAdminDAO
    {
        RechargeMobileEntities entities;
        public IList<TransactionAdminVM> TransactionList()
        {
            entities = new RechargeMobileEntities();
            List<TransactionAdminVM> list = new List<TransactionAdminVM>();
            var listTrans = entities.Transactions.OrderBy(d => d.DateTime).ToList();
            foreach(Transaction t in listTrans)
            {
                string typePacket;
                string rechargeName;
                decimal price;
                if (t.RRechargeId >= 1)
                {
                    var recharge = entities.RegularRecharges.Where(d => d.RRechargeId == t.RRechargeId).FirstOrDefault();
                    typePacket = "Regular";
                    rechargeName = recharge.RRName;
                    price = recharge.Price;
                }
                else
                {
                    var recharge = entities.SpecialRecharges.Where(d => d.SRechargeId == t.SRechargeId).FirstOrDefault();
                    typePacket = "Special";
                    rechargeName = recharge.SRName;
                    price = recharge.Price;
                }
                TransactionAdminVM item = new TransactionAdminVM()
                {
                    TransactionId = t.TransactionId,
                    PhoneNumber = t.PhoneNumber,
                    PaymentMethod = t.PaymentMethod,
                    RRechargeId = t.RRechargeId,
                    SRechargeId = t.SRechargeId,
                    RechargeType = typePacket,
                    RechargeName = rechargeName,
                    Price = price,
                    DateTime = t.DateTime,
                    Status = t.Status
                };
                list.Add(item);
            }
            return list;
        }

        public Tuple<IList<TransactionAdminVM>, decimal> TransactionListByDate(DateTime date)
        {
            entities = new RechargeMobileEntities();
            IList<TransactionAdminVM> list = new List<TransactionAdminVM>();
            var listTransRaw = entities.Transactions.OrderBy(d => d.DateTime).ToList();
            var listTrans = listTransRaw.Where(d => d.DateTime.Date == date.Date).ToList();
            decimal totalAmount = 0;
            foreach (Transaction t in listTrans)
            {
                string typePacket;
                string rechargeName;
                decimal price;
                if (t.RRechargeId >= 1)
                {
                    var recharge = entities.RegularRecharges.Where(d => d.RRechargeId == t.RRechargeId).FirstOrDefault();
                    typePacket = "Regular";
                    rechargeName = recharge.RRName;
                    price = recharge.Price;
                }
                else
                {
                    var recharge = entities.SpecialRecharges.Where(d => d.SRechargeId == t.SRechargeId).FirstOrDefault();
                    typePacket = "Special";
                    rechargeName = recharge.SRName;
                    price = recharge.Price;
                }
                TransactionAdminVM item = new TransactionAdminVM()
                {
                    TransactionId = t.TransactionId,
                    PhoneNumber = t.PhoneNumber,
                    PaymentMethod = t.PaymentMethod,
                    RRechargeId = t.RRechargeId,
                    SRechargeId = t.SRechargeId,
                    RechargeType = typePacket,
                    RechargeName = rechargeName,
                    Price = price,
                    DateTime = t.DateTime,
                    Status = t.Status
                };
                totalAmount += price;
                list.Add(item);
            }
            return Tuple.Create(list, totalAmount);
        }

        public Tuple<List<TransactionAdminVM>, decimal> AdvanceSearch(AdvanceSearchVM vm)
        {
            entities = new RechargeMobileEntities();           
            var listTrans = entities.Transactions.OrderBy(d => d.DateTime).ToList();
            var listTransByTime = listTrans.Where(d => d.DateTime >= vm.DateFrom.Date && d.DateTime < vm.DateTo.Date.AddDays(1)).ToList();

            List<Transaction> listTransByTimeType = new List<Transaction>();
            if (vm.RechargeType == "Regular")
            {
                listTransByTimeType = listTransByTime.Where(d => d.RRechargeId >= 1).ToList();
            }
            else if (vm.RechargeType == "Special")
            {
                listTransByTimeType = listTransByTime.Where(d => d.SRechargeId >= 1).ToList();
            }
            else
            {
                listTransByTimeType = listTransByTime.ToList();
            }

            List<Transaction> listTransByTimeTypePayment = new List<Transaction>();
            if (vm.PaymentMethod == "Paypal")
            {
                listTransByTimeTypePayment = listTransByTimeType.Where(d => d.PaymentMethod == "Paypal").ToList();
            }
            else if (vm.PaymentMethod == "Debit")
            {
                listTransByTimeTypePayment = listTransByTimeType.Where(d => d.PaymentMethod == "Debit").ToList();
            }
            else
            {
                listTransByTimeTypePayment = listTransByTimeType.ToList();
            }

            List<Transaction> listTransByTimeTypePaymentStaus = new List<Transaction>();
            if(vm.Status == "Paid")
            {
                listTransByTimeTypePaymentStaus = listTransByTimeTypePayment.Where(d => d.Status == "Paid").ToList();
            }
            else if (vm.Status == "Unpaid")
            {
                listTransByTimeTypePaymentStaus = listTransByTimeTypePayment.Where(d => d.Status == "Unpaid").ToList();
            }
            else
            {
                listTransByTimeTypePaymentStaus = listTransByTimeTypePayment.ToList();
            }

            List<Transaction> listTransByTimeTypePaymentStausPhone = new List<Transaction>();
            if (vm.PhoneNumber != null)
            {
                listTransByTimeTypePaymentStausPhone = listTransByTimeTypePaymentStaus.Where(d => d.PhoneNumber.Contains(vm.PhoneNumber)).ToList();
            }
            else
            {
                listTransByTimeTypePaymentStausPhone = listTransByTimeTypePaymentStaus.ToList();
            }

            List<TransactionAdminVM> list = new List<TransactionAdminVM>();
            decimal totalAmount = 0;
            foreach (Transaction t in listTransByTimeTypePaymentStausPhone)
            {
                string typePacket;
                string rechargeName;
                decimal price;
                if (t.RRechargeId >= 1)
                {
                    var recharge = entities.RegularRecharges.Where(d => d.RRechargeId == t.RRechargeId).FirstOrDefault();
                    typePacket = "Regular";
                    rechargeName = recharge.RRName;
                    price = recharge.Price;
                }
                else
                {
                    var recharge = entities.SpecialRecharges.Where(d => d.SRechargeId == t.SRechargeId).FirstOrDefault();
                    typePacket = "Special";
                    rechargeName = recharge.SRName;
                    price = recharge.Price;
                }
                TransactionAdminVM item = new TransactionAdminVM()
                {
                    TransactionId = t.TransactionId,
                    PhoneNumber = t.PhoneNumber,
                    PaymentMethod = t.PaymentMethod,
                    RRechargeId = t.RRechargeId,
                    SRechargeId = t.SRechargeId,
                    RechargeType = typePacket,
                    RechargeName = rechargeName,
                    Price = price,
                    DateTime = t.DateTime,
                    Status = t.Status
                };
                totalAmount += price;
                list.Add(item);
            }
            return Tuple.Create(list,totalAmount);
        }
    }
}