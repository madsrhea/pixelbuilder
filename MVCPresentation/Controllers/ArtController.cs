using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class ArtController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private LogicLayer.ArtManager artManager = new LogicLayer.ArtManager();

        // GET: Art
        public ActionResult Gallery()
        {
            var allArt = artManager.RetrieveAllArt();

            List<ArtGalleryViewModel> artViewModels = new List<ArtGalleryViewModel>();
            List<Art> arts;
            try
            {
                arts = artManager.RetrieveAllArt();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

            foreach (var art in arts)
            {
                ArtGalleryViewModel artViewModel = new ArtGalleryViewModel();
                artViewModel.Art = art;
                try
                {
                    artViewModel.ArtImage = "/images/" + artViewModel.Art.ArtName + "big_" + artViewModel.Art.ArtID + ".png";
                }
                catch
                {
                    artViewModel.ArtImage = "/images/defaultIconbig_10011.png";
                }

                artViewModels.Add(artViewModel);
            }
            return View(artViewModels);

        }

        // GET: Art/Details/5
        public ActionResult Details(int? artID)
        {
            if (artID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Art art = artManager.RetrieveArtById((int)artID);

            if (art == null)
            {
                return HttpNotFound();
            }

            IndividualArtViewModel individVM = new IndividualArtViewModel();
            var dbContext = new ApplicationDbContext();
            var userManager = new LogicLayer.UserManager();
            var user = userManager.AuthenticateUser(User.Identity.GetUserName(), "newuser"); // bad practice i know :(

            individVM.Art = art;
            individVM.User = user;


            return View(individVM);
        }

        public ActionResult Edit(int? artID)
        {
            if (artID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Art art = artManager.RetrieveArtById((int)artID);

            if (art == null)
            {
                return HttpNotFound();
            }

            return View("EditArt", art);
        }

        // GET: Art/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, string artName, int artID)
        {
            string newName = "";

            if (file != null)
            {
                string path = HttpContext.Server.MapPath(@"~/images");

                bool exists = Directory.Exists(path);

                if (!exists)
                {
                    Directory.CreateDirectory(path);
                }

                string extention = Path.GetExtension(file.FileName);
                newName = artName.Trim() + "_" + artID + extention;
                string filePath = Path.Combine(path, newName);
                file.SaveAs(filePath);

            }

            return View();
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
