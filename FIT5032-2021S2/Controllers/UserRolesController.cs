using FIT5032_2021S2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        private UserManager<ApplicationUser> userManager;

        public UserRolesController()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
        }

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
        public ActionResult CreateRole(string RoleName)
        {
            try
            {
                var newRole = new IdentityRole() { Name = RoleName };
                context.Roles.Add(newRole);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
                return View();
            }
        }

        // GET: UserRoles/DeleteRole
        public ActionResult DeleteRole(string roleName)
        {
            var role = context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: UserRoles/EditRole
        [HttpPost]
        public ActionResult EditRole(IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(role); }
        }

        // GET: UserRoles/AddRoleToUser
        public ActionResult AddRoleToUser()
        {
            ViewBag.Roles = context.Roles.ToList().Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Users = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
            return View();
        }

        // POST: UserRoles/AddRoleToUser
        [HttpPost]
        public ActionResult AddRoleToUser(string Email, string Role)
        {
            try
            {
                var user = userManager.FindByEmail(Email);
                if (user != null)
                    userManager.AddToRole(user.Id, Role);
                else
                    ModelState.AddModelError("Error", "Email required.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error: ", e.Message);
            }
            ViewBag.Roles = context.Roles.ToList().Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.Users = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
            return View();
        }

        // GET: UserRoles/GetUserRoles
        public ActionResult GetUserRoles()
        {
            ViewBag.Users = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
            return View();
        }

        // PSOT: UserRoles/GetUserRoles
        [HttpPost]
        public ActionResult GetUserRoles(string Email)
        {
            try
            {
                ViewBag.Email = Email;
                var user = userManager.FindByEmail(Email);
                if (user != null) 
                    ViewBag.Roles = userManager.GetRoles(user.Id);
                else 
                    ModelState.AddModelError("Error", "Email required.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message);
            }
            ViewBag.Users = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
            return View();
        }

        // GET: UserRoles/DeleteUserRole
        public ActionResult DeleteUserRole(string Email, string Role) {
            var user = userManager.FindByEmail(Email);
            if (this.userManager.IsInRole(user.Id, Role))
                this.userManager.RemoveFromRole(user.Id, Role);
            return RedirectToAction("Index");
        }



    }
}