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
        
        //public Tuple<AdminCustomerDetailsVM, List<TransactionAdminVM>> GetCustomerDetails(int id)
        //{
        //    entities = new RechargeMobileEntities();
        //    Customer customer = entities.Customers.Where(d => d.CustomerId == id).FirstOrDefault();
        //    CustomerRecharge customerRecharge = entities.CustomerRecharges.Where(d => d.PhoneNumber == customer.PhoneNumber).FirstOrDefault();
        //    AdminCustomerDetailsVM customerDetaits = new AdminCustomerDetailsVM()
        //    {
        //        CustomerId = customer.CustomerId,
        //        PhoneNumber = customer.PhoneNumber,
        //        FirstName = customer.FirstName,
        //        LastName = customer.LastName,
        //        Email = customer.Email,
        //        Address = customer.Address,
        //        Busy = customer.Busy,
        //        Lock = customer.Lock,
        //        Status = customer.Status,
        //    };
        //    customerDetaits.TimeRemain = Decimal.ToInt32(customerRecharge.TimeRemain/60);
        //    customerDetaits.RegularTime = customerDetaits.RegularTime;
        //    customerDetaits.SpecialTime = customerRecharge.SpecialTime;
        //    customerDetaits.TimeToPay = customerRecharge.TimeToPay;
        //}

    }
}