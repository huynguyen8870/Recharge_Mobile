using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class DebitTransactionModelView
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public DebitTransactionModelView()
        {

        }

        public DebitTransactionModelView(int id, int transactionId, DateTime dateTime, string status)
        {
            Id = id;
            TransactionId = transactionId;
            DateTime = dateTime;
            Status = status;
        }
    }
}