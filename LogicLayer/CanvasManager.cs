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
    public class CanvasManager : ICanvasManager
    {
        private ICanvasAccessor _canvasAccessor = null;

        public CanvasManager()
        {
            _canvasAccessor = new DataAccessLayer.CanvasAccessor();
        }

        public CanvasManager(ICanvasAccessor canvasAccessor)
        {
            _canvasAccessor = canvasAccessor;
        }

        public List<Canvas> RetrieveCanvasPlacementByArtID(int artID)
        {
            List<Canvas> canvas = new List<Canvas>();

            try
            {
                canvas = _canvasAccessor.SelectCanvasByArtID(artID);
            }
            catch (Exception up)
            {
                throw up;
            }

            return canvas;
        }
    }
}
