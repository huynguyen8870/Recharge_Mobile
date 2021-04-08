using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminCustomerListVM
    {
        public AdminCustomerListVM()
        {

        }

        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Busy { get; set; }
        public string Lock { get; set; }
        public string Status { get; set; }
    }
}