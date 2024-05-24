using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IFavoriteManager
    {
        // CREATE - INSERT
        int CreateFavorite(int userID, int artID);

        // READ - SELECT
        List<Art> RetrieveAllUserFavorites(int userID);
        bool ConfirmFavorite(int userID, int artID);

        // UPDATE

        // DEACTIVATE
        int RemoveFavorite(int userID, int artID);
    }
}
