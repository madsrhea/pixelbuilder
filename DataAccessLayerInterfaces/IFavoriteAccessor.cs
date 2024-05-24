using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IFavoriteAccessor
    {
        // CREATE - INSERT
        int InsertFavorite(int userID, int artID);

        // READ - SELECT
        List<Art> SelectAllUserFavorites(int userID);
        int AuthenticateFavorite(int userID, int artID);

        // UPDATE

        // DEACTIVATE
        int DeleteFavorite(int userID, int artID);
    }
}
