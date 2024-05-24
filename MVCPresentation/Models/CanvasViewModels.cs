using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class LoadCanvasPlacementViewModel
    {
        public Canvas Canvas { get; set; }
        public Bead Bead { get; set; }
        public string HexColor { get; set; }
    }

    public class NewCanvasAndBeadsViewModel
    { 
        public List<Bead> Bead { get; set; }
        
    }
}