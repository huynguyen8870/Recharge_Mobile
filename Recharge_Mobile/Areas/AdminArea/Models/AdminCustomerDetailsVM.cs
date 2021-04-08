using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminCustomerDetailsVM
    {
        public AdminCustomerDetailsVM()
        {

        }

        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }

        public int TimeRemain { get; set; }
        public DateTime RegularTime { get; set; }
        public DateTime SpecialTime { get; set; }
        public DateTime TimeToPay { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Busy { get; set; }
        public string Lock { get; set; }
        public string Status { get; set; }

    }
}