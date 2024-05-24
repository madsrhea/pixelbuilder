using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class ArtAccessor : IArtAccessor
    {

        public int InsertArt(Art art)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_insert_art", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", art.UserID);
            cmd.Parameters.AddWithValue("@ArtName", art.ArtName);
            cmd.Parameters.AddWithValue("@Description", art.Description);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return rows;
        }


        public Art SelectArtByID(int artID)
        {
            Art art = new Art();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmd = new SqlCommand("sp_select_art_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ArtID", artID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    art.ArtID = reader.GetInt32(0);
                    art.Username = reader.GetString(1);
                    art.ArtName = reader.GetString(2);
                    art.Description = reader.GetString(3);
                    art.PostedOn = reader.GetDateTime(4);
                    art.UpdatedOn = reader.IsDBNull(5) ? reader.GetDateTime(4) : reader.GetDateTime(5);

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return art;
        }

        public int DeleteArtFromUser(int userID, int artID)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_art_from_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ArtID", SqlDbType.Int);

            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@ArtID"].Value = artID;

            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public List<Art> SelectAllArt()
        {
            List<Art> artList = new List<Art>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_all_art";
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
                        artList.Add(new Art()
                        {
                            ArtID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            ArtName = reader.GetString(2),
                            Description = reader.GetString(3),
                            PostedOn = reader.GetDateTime(4),
                            UpdatedOn = reader.IsDBNull(5) ? reader.GetDateTime(4) : reader.GetDateTime(5)

                        });
                    }
                    
                }

                reader.Close();

            }
            catch (Exception up)
            {

                throw up;
            }

            return artList;
        }

        public List<Art> SelectArtByUser(int userID)
        {
            List<Art> artList = new List<Art>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_art_by_userID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        artList.Add(new Art()
                        {
                            ArtID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            ArtName = reader.GetString(2),
                            Description = reader.GetString(3),
                            PostedOn = reader.GetDateTime(4)

                        });
                    }

                }

                reader.Close();

            }
            catch (Exception up)
            {

                throw up;
            }

            return artList;
        }

        public int UpdateArtDescription(int artID, string description)
        {
                int rows = 0;

                DBConnection connectionFactory = new DBConnection();
                var conn = connectionFactory.GetConnection();

                var cmdText = "sp_update_art_description";

                var cmd = new SqlCommand(cmdText, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("ArtID", artID);
                cmd.Parameters.AddWithValue("Description", description);

                try
                {
                    conn.Open();
                    rows = cmd.ExecuteNonQuery();
                }
                catch (Exception up)
                {
                    throw up;
                }
                finally
                {
                    conn.Close();
                }

                return rows;
            }

    }
}
