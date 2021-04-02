using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.RechargeArea.Models
{
    public class RegularRechargeDAO
    {
        static RechargeMobileEntities entities;

        public static void CreateItem(RegularRechargeVM vm)
        {
            entities = new RechargeMobileEntities();
            RegularRecharge newItem = new RegularRecharge()
            {
                RRName = vm.RRName,
                BaseTime = vm.BasteTimeMinute * 60,
                BonusTime = vm.BonusTimeMinute * 60,
                TotalTime = (vm.BasteTimeMinute + vm.BonusTimeMinute) * 60,
                Price = vm.Price,
                Duration = vm.DurationDay * 86400,
                Description = vm.Description,
                Status = "Active"
            };
            entities.RegularRecharges.Add(newItem);
            entities.SaveChanges();
        }

        public static IList<RegularRechargeVM> GetList()
        {
            entities = new RechargeMobileEntities();
            var listRaw = entities.RegularRecharges.ToList();
            var list = listRaw.Select(d => new RegularRechargeVM
            {
                RRechargeId = d.RRechargeId,
                RRName = d.RRName,
                BasteTimeMinute = Decimal.ToInt32(d.BaseTime / 60),
                BonusTimeMinute = Decimal.ToInt32(d.BonusTime / 60),
                TotalTimeMinute = Decimal.ToInt32(d.TotalTime / 60),
                Price = d.Price,
                DurationDay = Decimal.ToInt32(d.Duration / 86400),
                Description = d.Description,
                Status = d.Status
            }).ToList();
            return list;
        }

        public static IList<RegularRechargeVM> GetListActive()
        {
            entities = new RechargeMobileEntities();
            var listRaw = entities.RegularRecharges.Where(d => d.Status == "Active").ToList();
            var list = listRaw.Select(d => new RegularRechargeVM
            {
                RRechargeId = d.RRechargeId,
                RRName = d.RRName,
                BasteTimeMinute = Decimal.ToInt32(d.BaseTime / 60),
                BonusTimeMinute = Decimal.ToInt32(d.BonusTime / 60),
                TotalTimeMinute = Decimal.ToInt32(d.TotalTime / 60),
                Price = d.Price,
                DurationDay = Decimal.ToInt32(d.Duration / 86400),
                Description = d.Description,
                Status = d.Status
            }).ToList();
            return list;
        }

        public static RegularRechargeVM GetItemById(int id)
        {
            entities = new RechargeMobileEntities();
            var itemRaw = entities.RegularRecharges.Where(d => d.RRechargeId == id).FirstOrDefault();
            RegularRechargeVM item = new RegularRechargeVM()
            {
                RRechargeId = itemRaw.RRechargeId,
                RRName = itemRaw.RRName,
                BasteTimeMinute = Decimal.ToInt32(itemRaw.BaseTime / 60),
                BonusTimeMinute = Decimal.ToInt32(itemRaw.BonusTime / 60),
                TotalTimeMinute = Decimal.ToInt32(itemRaw.TotalTime / 60),
                Price = itemRaw.Price,
                DurationDay = Decimal.ToInt32(itemRaw.Duration / 86400),
                Description = itemRaw.Description,
                Status = itemRaw.Status
            };
            return item;
        }

        public static void EditItem(RegularRechargeVM vm)
        {
            entities = new RechargeMobileEntities();
            var item = entities.RegularRecharges.Where(d => d.RRechargeId == vm.RRechargeId).FirstOrDefault();
            item.RRName = vm.RRName;
            item.BaseTime = vm.BasteTimeMinute * 60;
            item.BonusTime = vm.BonusTimeMinute * 60;
            item.TotalTime = (vm.BasteTimeMinute + vm.BonusTimeMinute) * 60;
            item.Price = vm.Price;
            item.Duration = vm.DurationDay * 86400;
            item.Description = vm.Description;
            entities.SaveChanges();
        }

        public static void ActivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.RegularRecharges.Where(d => d.RRechargeId == id).FirstOrDefault();
            item.Status = "Active";
            entities.SaveChanges();
        }

        public static void DeactivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.RegularRecharges.Where(d => d.RRechargeId == id).FirstOrDefault();
            item.Status = "Inactive";
            entities.SaveChanges();
        }
    }
}