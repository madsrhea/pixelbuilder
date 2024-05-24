using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class FavoriteViewModel
    {
        public List<Art> Favorites { get; set; }
        public User User { get; set; }
    }

    public class FollowViewModel
    {
        public User User { get; set; }
        public List<User> Following { get; set; }
        // think of it like the IDs of their Following (the people following them)
    }
}