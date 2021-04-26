﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Models.Views
{
    public class PostPaymentModelView
    {
        public PostPaymentModelView()
        {
                
        }

        public int TransactionId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public PostPaymentModelView(int transactionId, DateTime dateTime, decimal amount, string status)
        {
            TransactionId = transactionId;
            DateTime = dateTime;
            Amount = amount;
            Status = status;
        }
    }
}