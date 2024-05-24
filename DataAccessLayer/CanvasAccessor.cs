using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CanvasAccessor : ICanvasAccessor
    {
        public List<Canvas> SelectCanvasByArtID(int artID) 
        {
            List<Canvas> canvases = new List<Canvas>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_canvas_by_artID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ArtID", SqlDbType.Int);

            cmd.Parameters["@ArtID"].Value = artID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var canvas = new Canvas();

                        canvas.ArtID = reader.GetInt32(0);
                        canvas.Row = reader.GetInt32(1);
                        canvas.Column = reader.GetInt32(2);
                        canvas.BeadID = reader.GetString(3);

                        canvases.Add(canvas);
                    }
                }

            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return canvases;
        }
    }
}
