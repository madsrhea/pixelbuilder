using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IBeadManager
    {
        List<Bead> RetrieveAllBeads();
        Bead RetrieveBeadById(string beadID);
        Bead RetrieveBeadByName(string beadName);
        List<string> RetrieveAllColorGroups();
        List<Bead> RetrieveBeadsByColorGroup(string colorGroupName);
    }
}
