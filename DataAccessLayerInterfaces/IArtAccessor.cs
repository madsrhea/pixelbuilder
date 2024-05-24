using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerInterfaces
{
    public interface IArtAccessor
    {
        // CREATE - INSERT
        int InsertArt(Art art);

        // READ - SELECT
        List<Art> SelectAllArt();
        List<Art> SelectArtByUser(int userID);
        Art SelectArtByID(int artID);

        // UPDATE
        int UpdateArtDescription(int artID, string description);

        // DELETE - DEACTIVATE
        int DeleteArtFromUser(int userId, int artID);
    }
}
