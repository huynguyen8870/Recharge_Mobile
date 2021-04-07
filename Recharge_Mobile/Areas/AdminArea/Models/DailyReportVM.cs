using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class DailyReportVM
    {
        public DailyReportVM()
        {

        }

        public IList<TransactionAdminVM> TransactionList { get; set; }
    }
}