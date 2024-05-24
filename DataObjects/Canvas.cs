using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataObjects
{
    public class Canvas
    {
        public int CanvasID { get; set; }
        public int ArtID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string BeadID { get; set; }
    }

    public class CanvasArt : Canvas 
    {
        public string Username { get; set; }
    }

    public class CanvasTagPost : Canvas
    { 
        public string TagName { get; set; }
    }
}
