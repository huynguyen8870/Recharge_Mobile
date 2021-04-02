using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.RechargeArea.Models
{
    public class SpecialRechargeDAO
    {
        static RechargeMobileEntities entities;

        public static void CreateItem(SpecialRechargeVM vm)
        {
            entities = new RechargeMobileEntities();
            SpecialRecharge newItem = new SpecialRecharge()
            {
                SRName = vm.SRName,
                Price = vm.Price,
                Duration = vm.DurationDay * 86400,
                Description = vm.Description,
                Status = "Active"
            };
            entities.SpecialRecharges.Add(newItem);
            entities.SaveChanges();
        }

        public static IList<SpecialRechargeVM> GetList()
        {
            entities = new RechargeMobileEntities();
            var listRaw = entities.SpecialRecharges.ToList();
            var list = listRaw.Select(d => new SpecialRechargeVM
            {
                SRechargeId = d.SRechargeId,
                SRName = d.SRName,
                Price = d.Price,
                DurationDay = Decimal.ToInt32(d.Duration / 86400),
                Description = d.Description,
                Status = d.Status
            }).ToList();
            return list;
        }

        public static IList<SpecialRechargeVM> GetListActive()
        {
            entities = new RechargeMobileEntities();
            var listRaw = entities.SpecialRecharges.Where(d => d.Status == "Active").ToList();
            var list = listRaw.Select(d => new SpecialRechargeVM
            {
                SRechargeId = d.SRechargeId,
                SRName = d.SRName,
                Price = d.Price,
                DurationDay = Decimal.ToInt32(d.Duration / 86400),
                Description = d.Description,
                Status = d.Status
            }).ToList();
            return list;
        }

        public static SpecialRechargeVM GetItemById(int id)
        {
            entities = new RechargeMobileEntities();
            var itemRaw = entities.SpecialRecharges.Where(d => d.SRechargeId == id).FirstOrDefault();
            SpecialRechargeVM item = new SpecialRechargeVM()
            {
                SRechargeId = itemRaw.SRechargeId,
                SRName = itemRaw.SRName,
                Price = itemRaw.Price,
                DurationDay = Decimal.ToInt32(itemRaw.Duration / 86400),
                Description = itemRaw.Description,
                Status = itemRaw.Status
            };
            return item;
        }

        public static void EditItem(SpecialRechargeVM vm)
        {
            entities = new RechargeMobileEntities();
            var item = entities.SpecialRecharges.Where(d => d.SRechargeId == vm.SRechargeId).FirstOrDefault();
            item.SRName = vm.SRName;
            item.Price = vm.Price;
            item.Duration = vm.DurationDay * 86400;
            item.Description = vm.Description;
            entities.SaveChanges();
        }

        public static void ActivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.SpecialRecharges.Where(d => d.SRechargeId == id).FirstOrDefault();
            item.Status = "Active";
            entities.SaveChanges();
        }

        public static void DeactivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.SpecialRecharges.Where(d => d.SRechargeId == id).FirstOrDefault();
            item.Status = "Inactive";
            entities.SaveChanges();
        }
    }
}