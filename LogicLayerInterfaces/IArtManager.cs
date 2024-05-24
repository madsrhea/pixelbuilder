using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IArtManager
    {
        // CREATE - INSERT
        bool AddArt(Art art);

        // READ - SELECT
        List<Art> RetrieveAllArt();
        List<Art> RetrieveArtByUser(int userID);
        Art RetrieveArtById(int artId);

        // UPDATE
        bool UpdateArtDescription(int artID, string description);

        // DELETE - DEACTIVATE
        bool DeleteArtFromUser(int userID, int artID);
    }
}
