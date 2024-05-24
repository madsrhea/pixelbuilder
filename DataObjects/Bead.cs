using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Bead
    {
        public string BeadID { get; set; }
        public string ColorName { get; set; }
        public int ColorGroupID { get; set; }
        public int BrandID { get; set; }
        public string HexValue { get; set; }
    }

    public class BeadColorGroup : Bead
    {
        public string ColorGroupName { get; set; }
    }

    public class BeadBrand : Bead
    {
        public string BrandName { get; set; }
    }
}
