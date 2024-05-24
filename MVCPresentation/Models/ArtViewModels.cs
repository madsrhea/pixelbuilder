using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class ArtGalleryViewModel
    {
        public Art Art { get; set; }
        public string ArtImage { get; set; }
    }

    public class IndividualArtViewModel
    {
        public Art Art { get; set; }
        public User User { get; set; }
    }

}