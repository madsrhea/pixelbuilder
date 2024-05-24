using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface ICanvasAccessor
    {

        // CREATE - INSERT

        // READ - SELECT
        List<Canvas> SelectCanvasByArtID(int artID);

        // UPDATE

        // DELETE - DEACTIVATE


    }
}
