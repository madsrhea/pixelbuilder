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
    public class BeadAccessor : IBeadAccessor
    {
        public List<Bead> SelectAllBeads()
        {
            List<Bead> beads = new List<Bead>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_beads";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Bead bead = new Bead();

                        bead.BeadID = reader.GetString(0);
                        bead.ColorName = reader.GetString(1);
                        bead.ColorGroupID = reader.GetInt32(2);
                        bead.BrandID = reader.GetInt32(3);
                        bead.HexValue = reader.GetString(4);

                        beads.Add(bead);
                    }
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return beads;
        }

        public Bead SelectBeadById(string beadID)
        {
            Bead bead = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_bead_by_beadID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BeadID", SqlDbType.NVarChar, 25);

            cmd.Parameters["@BeadID"].Value = beadID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    bead = new Bead();

                    bead.BeadID = reader.GetString(0);
                    bead.ColorName = reader.GetString(1);
                    bead.HexValue = reader.GetString(2);

                }
                reader.Close();
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return bead;
        }

        public Bead SelectBeadByName(string beadName)
        {
            Bead bead = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_bead_by_name";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ColorName", SqlDbType.NVarChar, 100);

            cmd.Parameters["@ColorName"].Value = beadName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    bead = new Bead();

                    bead.ColorName = reader.GetString(0);
                    bead.HexValue = reader.GetString(1);

                }
                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return bead;
        }

        public List<Bead> SelectBeadsByColorGroup(string colorGroupName)
        {
            List<Bead> beads = new List<Bead>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_beads_by_colorgroup";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ColorGroupName", SqlDbType.NVarChar, 15);

            cmd.Parameters["@ColorGroupName"].Value = colorGroupName;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Bead bead = new Bead();

                        bead.BeadID = reader.GetString(0);
                        bead.ColorName = reader.GetString(1);
                        bead.ColorGroupID = reader.GetInt32(2);
                        bead.BrandID = reader.GetInt32(3);
                        bead.HexValue = reader.GetString(4);

                        beads.Add(bead);
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

            return beads;
        }

        public List<string> SelectAllColorGroups()
        {
            List<string> colorGroups = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_colorgroups";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        colorGroups.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot retrieve roles.");
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return colorGroups;
        }
    }
}
