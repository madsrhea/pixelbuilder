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
    public class FollowerAccessor : IFollowerAccessor
    {
        // CREATE - INSERT
        public int InsertFollow(int userID, int followingID)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_follow";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@FollowingID", followingID);

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
        public List<User> SelectAllUserFollowers(int userID)
        {
            List<User> userList = new List<User>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_user_followers";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        userList.Add(new User()
                        {
                            UserID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            DisplayName = reader.GetString(2),
                            ShortBio = reader.GetString(3),
                            Email = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            Active = reader.GetBoolean(6)

                        });
                    }
                }

                reader.Close();

            }
            catch (Exception up)
            {

                throw up;
            }

            return userList;
        }

        public List<User> SelectAllUsersFollowing(int followingID)
        {
            List<User> userList = new List<User>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_users_following";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FollowingID", followingID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        userList.Add(new User()
                        {
                            UserID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            DisplayName = reader.GetString(2),
                            ShortBio = reader.GetString(3),
                            Email = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            Active = reader.GetBoolean(6)

                        });
                    }
                }

                reader.Close();

            }
            catch (Exception up)
            {

                throw up;
            }

            return userList;
        }


        // UPDATE

        // DEACTIVATE
        public int DeleteFollow(int userID, int followingID)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_delete_follow";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@FollowingID", followingID);

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

        public int AuthenticateFollow(int followingID, int userID)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_authenticate_favorite";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FollowingID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@FollowingID"].Value = followingID;
            cmd.Parameters["@UserID"].Value = userID;

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
    }
}
