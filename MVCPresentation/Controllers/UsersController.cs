using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult UserProfile()
        {

            return View();
        }

        public ActionResult ViewUserGallery(int? userID)
        {
            try
            {
                LogicLayer.ArtManager artManager = new LogicLayer.ArtManager();
                List<Art> userArt = artManager.RetrieveArtByUser((int)userID);
                var userManager = new LogicLayer.UserManager();
                User user = userManager.RetrieveUserByUserID((int)userID);

                FavoriteViewModel favVM = new FavoriteViewModel();

                favVM.Favorites = userArt; /* I didn't name this well LMAO */
                favVM.User = user;

                return View("_UserGallery", favVM);
            }
            catch (HttpException)
            {
                return View("_UserGallery");
            }
            catch (Exception up)
            {
                ViewBag.Message = up.Message;
                return View("Error");
            }
        }

        
        public ActionResult ViewFavorites(int? userID)
        {
            try
            {
                if (userID == null)
                {
                    return View("_UserFavorites");
                }

                var favoriteManager = new LogicLayer.FavoriteManager();
                var userManager = new LogicLayer.UserManager();

                List<DataObjects.Art> favoriteList = favoriteManager.RetrieveAllUserFavorites((int)userID);
                User user = userManager.RetrieveUserByUserID((int)userID);

                FavoriteViewModel favVM = new FavoriteViewModel();

                favVM.Favorites = favoriteList;
                favVM.User = user;

                return View("_UserFavorites", favVM);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }


        public ActionResult AddFavorite(int? artID)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(User.Identity.GetUserId());

            var favoriteManager = new LogicLayer.FavoriteManager();
            if (ModelState.IsValid)
            {
                try
                {
                    if (user == null)
                    {
                        ViewBag.Message = "Please sign in to use this feature.";
                        return RedirectToAction("Login", "Account");
                    }

                    else
                    {
                        favoriteManager.CreateFavorite((int)user.UserId, (int)artID);
                        return RedirectToAction("Details", "Art", artID);
                    }

                }
                catch (Exception up)
                {
                    ViewBag.Message = up.Message;
                    return View("Error");
                }
            }
            return View();
        }

        public ActionResult DeleteFavorite(int? artID)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userManager.FindById(User.Identity.GetUserId());

                var favoriteManager = new LogicLayer.FavoriteManager();

                favoriteManager.RemoveFavorite((int)user.UserId, (int)artID);
                return RedirectToAction("Details", "Art", artID);

            }
            catch (Exception)
            {
                throw;
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
