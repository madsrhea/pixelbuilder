using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Art
    {
        public int ArtID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string ArtName { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Hidden { get; set; }

    }

    public class ArtCanvas : Art
    { 
        public int CanvasID { get; set; }
    }

}
