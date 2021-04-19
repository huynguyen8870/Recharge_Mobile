using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminCustomerDAO
    {
        RechargeMobileEntities entities;
        public IList<AdminCustomerListVM> GetCustomerList()
        {
            entities = new RechargeMobileEntities();
            var list = entities.Customers.Select(d => new AdminCustomerListVM()
            {
                CustomerId = d.CustomerId,
                PhoneNumber = d.PhoneNumber,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Address = d.Address,
                Busy = d.Busy,
                Lock = d.Lock,
                Status = d.Status
            }).ToList();
            return list;
        }

        public Tuple<AdminCustomerDetailsVM, List<TransactionAdminVM>> GetCustomerDetails(int id)
        {
            entities = new RechargeMobileEntities();
            Customer customer = entities.Customers.Where(d => d.CustomerId == id).FirstOrDefault();
            CustomerRecharge customerRecharge = entities.CustomerRecharges.Where(d => d.PhoneNumber == customer.PhoneNumber).FirstOrDefault();
            AdminCustomerDetailsVM customerDetaits = new AdminCustomerDetailsVM()
            {
                CustomerId = customer.CustomerId,
                PhoneNumber = customer.PhoneNumber,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                Busy = customer.Busy,
                Lock = customer.Lock,
                Status = customer.Status,
            };
            customerDetaits.TimeRemain = Decimal.ToInt32(customerRecharge.TimeRemain / 60);
            customerDetaits.RegularTime = customerDetaits.RegularTime;
            customerDetaits.SpecialTime = customerRecharge.SpecialTime;
            customerDetaits.TimeToPay = customerRecharge.TimeToPay;

            List<TransactionAdminVM> list = new List<TransactionAdminVM>();
            var listTrans = entities.Transactions.Where(d => d.PhoneNumber == customer.PhoneNumber).OrderBy(d => d.DateTime).ToList();
            foreach (Transaction t in listTrans)
            {
                string typePacket;
                string rechargeName;
                decimal price;
                string postPayment = "";

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

                if (t.PaymentMethod == "Debit")
                {
                    if (t.Status == "Unpaid")
                    {
                        postPayment = "N/A";
                    }
                    else if (t.Status == "Paid")
                    {
                        postPayment = entities.DebitTransactions.Where(d => d.TransactionId == t.TransactionId).FirstOrDefault().DateTime.ToString();
                    }

                }
                else
                {
                    postPayment = "-";
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
                    Amount = t.Amount,
                    DateTime = t.DateTime,
                    PostPayment = postPayment,
                    Status = t.Status
                };
                list.Add(item);
            }
            return Tuple.Create(customerDetaits, list);
        }

        public void DeactivateCustomer(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Customers.Where(d => d.CustomerId == id).FirstOrDefault();
            item.Status = "Inactive";
            entities.SaveChanges();
        }

        public void ActivateCustomer(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Customers.Where(d => d.CustomerId == id).FirstOrDefault();
            item.Status = "Active";
            entities.SaveChanges();
        }

    }
}