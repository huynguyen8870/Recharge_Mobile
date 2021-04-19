using Recharge_Mobile.Areas.User.Models.DAO;
using Recharge_Mobile.Areas.User.Models.Views;
using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.Recharge.Models.DAO
{
    public class RechargeDAO
    {
        static RechargeMobileEntities entities;

        public List<RRechargeModelView> RRechargeList()
        {
            entities = new RechargeMobileEntities();
            var RRListRaw = entities.RegularRecharges.Where(d => d.Status == "Active").OrderBy(d => d.Price).ToList();
            var RRList = RRListRaw.Select(d => new RRechargeModelView()
            {
                RRechargeId = d.RRechargeId,
                RRName = d.RRName,
                BaseTime = Convert.ToInt64(d.BaseTime) / 60,
                BonusTime = Convert.ToInt64(d.BonusTime),
                TotalTime = Convert.ToInt64(d.TotalTime),
                Price = d.Price,
                Duration = Convert.ToInt64(d.Duration),
                Description = d.Description,
            }).ToList();
            return RRList;
        }

        public List<SRechargeModelView> SRechargeList()
        {
            entities = new RechargeMobileEntities();
            var SRListRaw = entities.SpecialRecharges.Where(d => d.Status == "Active").ToList();
            var SRList = SRListRaw.Select(d => new SRechargeModelView()
            {
                SRechargeId = d.SRechargeId,
                SRName = d.SRName,
                Duration = Convert.ToInt64(d.Duration),
                Price = d.Price,
                Description = d.Description,
            }).ToList();
            return SRList;
        }

        public RRechargeModelView RRechargeById(int id)
        {
            entities = new RechargeMobileEntities();
            var RRechargeRaw = entities.RegularRecharges.Where(d => d.RRechargeId == id).FirstOrDefault();
            RRechargeModelView RRecharge = new RRechargeModelView()
            {
                RRName = RRechargeRaw.RRName,
                BaseTime = Convert.ToInt64(RRechargeRaw.BaseTime),
                BonusTime = Convert.ToInt64(RRechargeRaw.BonusTime),
                TotalTime = Convert.ToInt64(RRechargeRaw.TotalTime),
                Price = RRechargeRaw.Price,
                Duration = Convert.ToInt64(RRechargeRaw.Duration)
            };
            return RRecharge;
        }

        public SRechargeModelView SRechargeById(int id)
        {
            entities = new RechargeMobileEntities();
            var SRechargeRaw = entities.SpecialRecharges.Where(d => d.SRechargeId == id).FirstOrDefault();
            SRechargeModelView SRecharge = new SRechargeModelView()
            {
                SRName = SRechargeRaw.SRName,
                Duration = Convert.ToInt64(SRechargeRaw.Duration),
                Price = SRechargeRaw.Price,
            };
            return SRecharge;
        }

        public void CheckoutComplete(TransactionModelView transaction)
        {
            entities = new RechargeMobileEntities();

            //add data to table Transaction
            Transaction transactionEntity = new Transaction()
            {
                PhoneNumber = transaction.PhoneNumber,
                PaymentMethod = transaction.PaymentMethod,
                RRechargeId = transaction.RRechargeId,
                SRechargeId = transaction.SRechargeId,
                DateTime = transaction.DateTime,
                Status = transaction.Status,
                Amount = transaction.Amount
            };
            entities.Transactions.Add(transactionEntity);
            entities.SaveChanges();

            //add data to table CustomerRecharge
            long timeRemain = 0;
            long regularTime = 0;
            long specialTime = 0;

            if (transaction.RRechargeId > 0)
            {
                var rechargeInfo = RRechargeById(transaction.RRechargeId ?? 0);
                timeRemain = rechargeInfo.BaseTime;
                regularTime = rechargeInfo.Duration;
            }
            else
            {
                var rechargeInfo = SRechargeById(transaction.SRechargeId ?? 0);
                specialTime = rechargeInfo.Duration;
            };

            // check phonenumber is exist in table CustomerRecharge
            UserDAO userDAO = new UserDAO();
            var checkPhonenumber = userDAO.CheckPhonenumberInCustomerRecharge(transaction.PhoneNumber);
            if (checkPhonenumber == null)
            {
                CustomerRecharge customerRecharge = new CustomerRecharge()
                {
                    PhoneNumber = transaction.PhoneNumber,
                    TimeRemain = timeRemain,
                    RegularTime = DateTime.Now.AddDays(regularTime),
                    SpecialTime = DateTime.Now.AddDays(specialTime),
                    DebitAmount = 0,
                    TimeToPay = DateTime.Now,
                    LinkAccount = "No"
                };
                entities.CustomerRecharges.Add(customerRecharge);
                entities.SaveChanges();
            }
            else
            {
                //compare RegularTime with DateTime.Now
                DateTime regulartime;

                int compareRegularTime =checkPhonenumber.RegularTime.CompareTo(DateTime.Now);
                if (compareRegularTime < 0)
                {
                    regulartime = DateTime.Now.AddDays(regularTime);
                } else
                {
                    regulartime = checkPhonenumber.RegularTime.AddDays(regularTime);
                }

                //compare SpecialTime with DateTime.Now
                DateTime specialtime;

                int compareSpecialTime = checkPhonenumber.SpecialTime.CompareTo(DateTime.Now);
                if (compareSpecialTime < 0)
                {
                    specialtime = DateTime.Now.AddDays(specialTime);
                }
                else
                {
                    specialtime = checkPhonenumber.SpecialTime.AddDays(regularTime);
                }

                //check debit transaction
                decimal debitAmount = checkPhonenumber.DebitAmount;
                DateTime timeToPay ;

                //check TimeToPay
                int compareTimeToPay = checkPhonenumber.TimeToPay.CompareTo(DateTime.Now);
                if(compareTimeToPay < 0)
                {
                    timeToPay = DateTime.Now.AddDays(45);
                } else
                {
                    timeToPay = checkPhonenumber.TimeToPay.AddDays(45);
                }

                if(transaction.PaymentMethod == "Debit")
                {
                    debitAmount = debitAmount + transaction.Amount;
                }

                CustomerRechargeModelView customerRecharge = new CustomerRechargeModelView()
                {
                    PhoneNumber = checkPhonenumber.PhoneNumber,
                    TimeRemain = checkPhonenumber.TimeRemain + timeRemain,
                    RegularTime = regulartime,
                    SpecialTime = specialtime,
                    DebitAmount = debitAmount,
                    TimeToPay = timeToPay
                };
                userDAO.AddRechargeIntoCustomerRecharge(customerRecharge);
            }
        }

        public TransactionModelView TransactionById(int id)
        {
            string rechargename;
            decimal price;

            entities = new RechargeMobileEntities();
            var trasactionRaw = entities.Transactions.Where(d => d.TransactionId == id).FirstOrDefault();
            
            if(trasactionRaw.RRechargeId > 0)
            {
                var recharge = entities.RegularRecharges.Where(d => d.RRechargeId == trasactionRaw.RRechargeId).FirstOrDefault();
                rechargename = recharge.RRName;
                price = recharge.Price;
            } else
            {
                var recharge = entities.SpecialRecharges.Where(d => d.SRechargeId == trasactionRaw.SRechargeId).FirstOrDefault();
                rechargename = recharge.SRName;
                price = recharge.Price;
            }

            TransactionModelView transaction = new TransactionModelView()
            {
                TransactionId = trasactionRaw.TransactionId,
                PhoneNumber = trasactionRaw.PhoneNumber,
                PaymentMethod = trasactionRaw.PaymentMethod,
                RRechargeId = trasactionRaw.RRechargeId,
                SRechargeId = trasactionRaw.SRechargeId,
                RechargeName = rechargename,
                Price = price,
                DateTime = trasactionRaw.DateTime,
                Status = trasactionRaw.Status,
            };
            return transaction;
        }

        public void PaidTransaction(int id)
        {
            entities = new RechargeMobileEntities();
            //change status in table transaction
            var transaction = entities.Transactions.Where(d => d.TransactionId == id).FirstOrDefault();
            transaction.Status = "Paid";

            // edit debitAmount in table CustomerRecharge
            string phonenumber = transaction.PhoneNumber;
            var customerRecharge = entities.CustomerRecharges.Where(d => d.PhoneNumber == phonenumber).FirstOrDefault();
            customerRecharge.DebitAmount = customerRecharge.DebitAmount - transaction.Amount;

            entities.SaveChanges();
        }
    }
}