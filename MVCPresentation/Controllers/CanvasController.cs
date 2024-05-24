using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class CanvasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private LogicLayer.CanvasManager canvasManager = new LogicLayer.CanvasManager();
        private LogicLayer.BeadManager beadManager = new LogicLayer.BeadManager();
        private int canvasArraySize = 40;
        private int canvasPixelSize = 15;


        // GET: Canvas
        public ActionResult Index()
        {
            var colors = beadManager.RetrieveAllBeads();

            return View(colors);
        }

        // GET: Canvas
        public ActionResult Edit()
        {
            List<LoadCanvasPlacementViewModel> loadCanvasModels = new List<LoadCanvasPlacementViewModel>();
            List<Bead> beads = new List<Bead>();
            List<Canvas> canvases = new List<Canvas>();

            try
            {
                beads = beadManager.RetrieveAllBeads();
                canvases = canvasManager.RetrieveCanvasPlacementByArtID(1000019); // hard code in one with placements already - change later
                foreach (var canvas in canvases)
                {
                    LoadCanvasPlacementViewModel loadCanvasModel = new LoadCanvasPlacementViewModel();
                    loadCanvasModel.Canvas = canvas;
                    loadCanvasModels.Add(loadCanvasModel);
                }
            }
            catch (Exception)
            {

                throw;
            }


            return View(loadCanvasModels);
        }

        public ActionResult FilterColorGroup()
        {
            string selectedColorGroup = "";
            List<string> colorGroups = new List<string>();
            List<Bead> beads = new List<Bead>();
            try
            {
                colorGroups = beadManager.RetrieveAllColorGroups();
                switch (selectedColorGroup)
                {
                    case "Red & Pink":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 10).ToList();
                        break;
                    case "Purple":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 11).ToList();
                        break;
                    case "Blue":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 12).ToList();
                        break;
                    case "Green":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 13).ToList();
                        break;
                    case "Yellow":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 14).ToList();
                        break;
                    case "Orange":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 15).ToList();
                        break;
                    case "White":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 16).ToList();
                        break;
                    case "Grey":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 17).ToList();
                        break;
                    case "Black":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 18).ToList();
                        break;
                    case "Brown & Tan":
                        beads = beadManager.RetrieveAllBeads().Where(n => n.ColorGroupID == 19).ToList();
                        break;
                    default:
                        beads = beadManager.RetrieveAllBeads();
                        break;
                }

                //foreach (var bead in beads)
                //{ 
                    
                //}

                return View(beads);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Canvas> SaveArray()
        {
            List<Canvas> matrix = new List<Canvas>();

            //for (int i = 0; i <= canvasArraySize; i++)
            //{
            //    matrix.Add(new List<string>());
            //    matrix[i].Add(canvasArray[i][0]); /* ArtID */
            //    matrix[i].Add(canvasArray[i][1]); /* Row */
            //    matrix[i].Add(canvasArray[i][2]); /* Column */
            //    matrix[i].Add(canvasArray[i][3]); /* BeadID */

            //    if (canvasArray != null)
            //    {
            //        foreach (var bead in canvasArray)
            //        {

            //        }

            //    }
            //}
            return matrix;
        }
    }
}
