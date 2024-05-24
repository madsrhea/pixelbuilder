using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;



namespace LogicLayer
{
    public class ArtManager : IArtManager
    {
        private IArtAccessor artAccessor = null;

        public ArtManager()
        {
            artAccessor = new ArtAccessor();
        }

        public List<Art> RetrieveAllArt()
        {
            List<Art> artList = new List<Art>();

            try
            {
                artList = artAccessor.SelectAllArt();
            }
            catch (Exception up)
            {

                throw up;
            }

            return artList;
        
        }

        public List<Art> RetrieveArtByUser(int userID)
        {
            List<Art> artList = null;

            try
            {
                artList = artAccessor.SelectArtByUser(userID);
            }
            catch (Exception)
            {
                throw;
            }

            return artList;
        }

        public bool UpdateArtDescription(int artID, string description)
        {
            bool success = false;

            if (1 == artAccessor.UpdateArtDescription(artID, description))
            {
                success = true;
            }

            return success;
            
        }

        public bool DeleteArtFromUser(int userID, int artID)
        {
            bool success = false;

            if (1 == artAccessor.DeleteArtFromUser(userID, artID))
            {
                success = true;
            }

            return success;
        }

        public Art RetrieveArtById(int artID)
        {
            try
            {
                return artAccessor.SelectArtByID(artID);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Data not found.", up);
            }
        }

        public bool AddArt(Art art)
        {
            try
            {
                return 1 == artAccessor.InsertArt(art);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insertion Failed.", ex);
            }
        }
    }
}
