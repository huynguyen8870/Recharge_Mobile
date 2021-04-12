using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class DebitTransactionVM
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public DebitTransactionVM()
        {

        }

        public DebitTransactionVM(int id, int transactionId, DateTime dateTime, string status)
        {
            Id = id;
            TransactionId = transactionId;
            DateTime = dateTime;
            Status = status;
        }
    }
}