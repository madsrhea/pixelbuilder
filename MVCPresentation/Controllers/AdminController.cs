using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        // GET: Admin
        public ActionResult Index()
        {
            // return View(db.ApplicationUsers.ToList());

            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.Email).ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);

            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);

            
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var usrMgr = new LogicLayer.UserManager();
            var allRoles = usrMgr.RetrieveAllRoles();

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            // code so that the last admin cant be deleted
            if (role == "Admin")
            {
                var adminUsers = userManager.Users.ToList().Where(u => userManager.IsInRole(u.Id, "Admin")).ToList().Count();

                if (adminUsers < 2)
                {
                    ViewBag.Error = "You can't remove the only admin left!";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }

            }
            userManager.RemoveFromRole(id, role);

            if (user.UserId != null)
            {
                try
                {
                    var userMgr = new LogicLayer.UserManager();
                    userMgr.DeleteUserRole((int)user.UserId, role);
                }
                catch (Exception)
                {
                    // Nothing To Be Done
                }
            }

            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }

        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            userManager.AddToRole(id, role);

            if (user.UserId != null)
            {
                try
                {
                    var usrMgr = new LogicLayer.UserManager();
                    usrMgr.AddUserRole((int)user.UserId, role);

                }
                catch (Exception)
                {

                    // Nothing To Be Done
                }

            }

            return RedirectToAction("Details", "Admin", new { id = user.Id });

        }

    }
}
