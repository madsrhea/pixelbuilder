using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class BeadManager : IBeadManager
    {
        private IBeadAccessor _beadAccessor = null;
        private List<Bead> beads = null;

        public BeadManager()
        {
            _beadAccessor = new DataAccessLayer.BeadAccessor();
        }

        public BeadManager(IBeadAccessor beadAccessor)
        {
            _beadAccessor = beadAccessor;
        }

        public List<Bead> RetrieveAllBeads()
        {
            try
            {
                beads = _beadAccessor.SelectAllBeads();
            }
            catch (Exception up)
            {
                throw new ApplicationException("Beads not found.", up);
            }

            return beads;
        }

        public List<string> RetrieveAllColorGroups()
        {
            List<string> colorGroups = new List<string>();

            try
            {
                colorGroups = _beadAccessor.SelectAllColorGroups();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colorGroups;
        }

        public Bead RetrieveBeadById(string beadID)
        {
            Bead bead = null;

            try
            {
                bead = _beadAccessor.SelectBeadById(beadID);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Data not found.", up);
            }

            return bead;
        }

        public Bead RetrieveBeadByName(string beadName)
        {
            Bead bead = null;

            try
            {
                bead = _beadAccessor.SelectBeadById(beadName);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Data not found.", up);
            }

            return bead;
        }

        public List<Bead> RetrieveBeadsByColorGroup(string colorGroupName)
        {
            List<Bead> bead = new List<Bead>();

            try
            {
                bead = _beadAccessor.SelectBeadsByColorGroup(colorGroupName);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Data not found.", up);
            }

            return bead;
        }
    }
}
