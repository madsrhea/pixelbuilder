using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class FavoriteManager : IFavoriteManager
    {
        private IFavoriteAccessor _favoriteAccessor = null;

        public FavoriteManager()
        {
            _favoriteAccessor = new DataAccessLayer.FavoriteAccessor();
        }

        public FavoriteManager(IFavoriteAccessor favoriteAccessor)
        {
            _favoriteAccessor = favoriteAccessor;
        }

        // CREATE - INSERT
        public int CreateFavorite(int userID, int artID)
        {
            int result = 0;

            try
            {
                result = _favoriteAccessor.InsertFavorite(userID, artID);
            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }

        // READ - SELECT
        public List<Art> RetrieveAllUserFavorites(int userID)
        {
            List<Art> favorites = null;

            try
            {
                favorites = _favoriteAccessor.SelectAllUserFavorites(userID);
            }
            catch (Exception up)
            {

                throw up;
            }

            return favorites;
        }

        // UPDATE

        // DELETE - DEACTIVATE
        public int RemoveFavorite(int userID, int artID)
        {
            int result = 0;

            try
            {
                result = _favoriteAccessor.DeleteFavorite(userID, artID);
            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }

        public bool ConfirmFavorite(int userID, int artID)
        {
            bool result = false;

            try
            {
                result = (1 == _favoriteAccessor.AuthenticateFavorite(userID, artID));
            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }
    }
}
