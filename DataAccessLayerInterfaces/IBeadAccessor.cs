using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IBeadAccessor
    {

        // CREATE - INSERT

        // READ - SELECT
        List<Bead> SelectAllBeads();
        List<string> SelectAllColorGroups();
        List<Bead> SelectBeadsByColorGroup(string colorGroupName);
        Bead SelectBeadByName(string beadName);
        Bead SelectBeadById(string beadId);

        // UPDATE

        // DELETE - DEACTIVATE

    }
}
