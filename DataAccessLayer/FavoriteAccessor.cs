using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class FavoriteAccessor : IFavoriteAccessor
    {
        // CREATE - INSERT
        public int InsertFavorite(int userID, int artID)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_favorite_art";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@ArtID", artID);

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

        // READ - SELECT
        public List<Art> SelectAllUserFavorites(int userID)
        {
            List<Art> artList = new List<Art>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_user_favorites";
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

        public int AuthenticateFavorite(int userID, int artID)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_authenticate_favorite";

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
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return result;

        }

        // UPDATE

        // DEACTIVATE
        public int DeleteFavorite(int userID, int artID)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_delete_favorite";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@ArtID", artID);

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
