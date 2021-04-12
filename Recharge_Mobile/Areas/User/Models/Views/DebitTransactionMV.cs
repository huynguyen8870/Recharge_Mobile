using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.User.Models.Views
{
    public class DebitTransactionMV
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        public DebitTransactionMV()
        {

        }

        public DebitTransactionMV(int id, int transactionId, DateTime dateTime, string status)
        {
            Id = id;
            TransactionId = transactionId;
            DateTime = dateTime;
            Status = status;
        }
    }
}