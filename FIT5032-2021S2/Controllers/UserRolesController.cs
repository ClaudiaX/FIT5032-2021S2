using FIT5032_2021S2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_2021S2.Controllers
{
    public class UserRolesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: UserRoles
        public ActionResult Index()
        {
            var roles = this.context.Roles.ToList();
            return View(roles);
        }

        // GET: UserRoles/CreateRole
        public ActionResult CreateRole()
        {
            return View();
        }

        // POST: UserRoles/CreateRole
        [HttpPost]
        public ActionResult CreateRole(FormCollection formCollection)
        {
            try
            {
                var newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = formCollection["RoleName"]
                };
                context.Roles.Add(newRole);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteRole(string roleName)
        {
            var role = context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditRole(string roleName)
        {
            var role = context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(role); }
        }
    }
}