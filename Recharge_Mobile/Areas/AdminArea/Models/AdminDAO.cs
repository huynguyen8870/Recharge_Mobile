using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminDAO
    {
        static RechargeMobileEntities entities;
        public static void CreateAdmin(AdminVM vm)
        {
            entities = new RechargeMobileEntities();
            Admin item = new Admin()
            {
                //AdminId = vm.AdminId,
                Username = vm.Username,
                Password = vm.Password,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Address = vm.Address,
                Role = "Admin",
                Status = "Active"
            };
            entities.SaveChanges();
        }

        public static IList<AdminVM> ListAdmin()
        {
            entities = new RechargeMobileEntities();
            var list = entities.Admins.Select(d => new AdminVM()
            {
                AdminId = d.AdminId,
                Username = d.Username,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                Address = d.Address,
                Role = d.Role,
                Status = d.Status
            }).ToList();
            return list;
        }

        public static AdminVM GetAdminById(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.AdminId == id).Select(d => new AdminVM()
            {
                AdminId = d.AdminId,
                Username = d.Username,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                Address = d.Address,
                Role = d.Role,
                Status = d.Status
            }).FirstOrDefault();
            return item;
        }

        public static void EditAdmin(AdminVM vm)
        {
            entities = new RechargeMobileEntities();
            Admin item = entities.Admins.Where(d => d.AdminId == vm.AdminId).FirstOrDefault();
            item.FirstName = vm.FirstName;
            item.LastName = vm.LastName;
            item.PhoneNumber = vm.PhoneNumber;
            item.Email = vm.Email;
            item.Address = vm.Address;
            entities.SaveChanges();
        }

        public static void EditAdminPassword(int id, string newPassword)
        {
            entities = new RechargeMobileEntities();
            Admin item = entities.Admins.Where(d => d.AdminId == id).FirstOrDefault();
            item.Password = newPassword;
            entities.SaveChanges();
        }

        public static void ActivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.AdminId == id).FirstOrDefault();
            item.Status = "Active";
            entities.SaveChanges();
        }

        public static void DeactivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.AdminId == id).FirstOrDefault();
            item.Status = "Inactive";
            entities.SaveChanges();
        }
    }
}