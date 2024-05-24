using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Follower
    {
        public int UserID { get; set; }
        public int FollowingID { get; set; }
        public DateTime FollowedOn { get; set; }
    }
}
