using Recharge_Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recharge_Mobile.Areas.AdminArea.Models
{
    public class AdminDAO
    {
        RechargeMobileEntities entities;
        public void CreateAdmin(AdminVM vm)
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
            entities.Admins.Add(item);
            entities.SaveChanges();
        }

        public IList<AdminVM> ListAdmin()
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

        public AdminVM GetAdminById(int id)
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

        public AdminEditVM GetAdminEditById(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.AdminId == id).Select(d => new AdminEditVM()
            {
                AdminId = d.AdminId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                Address = d.Address
            }).FirstOrDefault();
            return item;
        }

        public void EditAdmin(AdminEditVM vm)
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

        public void EditAdminPassword(int id, string newPassword)
        {
            entities = new RechargeMobileEntities();
            Admin item = entities.Admins.Where(d => d.AdminId == id).FirstOrDefault();
            item.Password = newPassword;
            entities.SaveChanges();
        }

        public void ActivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.AdminId == id).FirstOrDefault();
            item.Status = "Active";
            entities.SaveChanges();
        }

        public void DeactivateItem(int id)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.AdminId == id).FirstOrDefault();
            item.Status = "Inactive";
            entities.SaveChanges();
        }

        public Boolean CheckUsernameExist(string username)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.Username.ToLower() == username.ToLower()).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            return true;
        }

        public Boolean CheckPhoneExist(string phone)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.PhoneNumber == phone).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            return true;
        }

        public Boolean CheckEmailExist(string email)
        {
            entities = new RechargeMobileEntities();
            var item = entities.Admins.Where(d => d.Email == email).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            return true;
        }
    }
}