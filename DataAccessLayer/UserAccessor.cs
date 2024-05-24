using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {

        //////////////////////////////////////////////////////////////////////
        ///////////// CREATE - INSERT METHODS //////////////
        //////////////////////////////////////////////////////////////////////

            // add new user to database
        public int InsertUser(User user)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_user";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@DisplayName", user.DisplayName);
            cmd.Parameters.AddWithValue("@Email", user.Email);

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


        //////////////////////////////////////////////////////////////////////
        ////////////// READ - SELECT METHODS /////////////////
        //////////////////////////////////////////////////////////////////////

            // view all users
        public List<User> SelectAllUsers()
        {
            List<User> userList = new List<User>();
    
            DBConnection connectionFactory = new DBConnection();

            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_all_users";

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

            // verify user is in database
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_authenticate_user";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

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

            // select user roles to display
        public string SelectRoleByUserID(int userID)
        {
            string role = "";

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_role_by_UserID";

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
                        role = reader.GetString(0);
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

            return role;
        }

            // select user by email
        public User SelectUserByEmail(string email)
        {
            User user = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_User_by_email";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    user = new User();

                    user.UserID = reader.GetInt32(0);
                    user.Username = reader.GetString(1);
                    user.DisplayName = reader.GetString(2);
                    user.ShortBio = reader.GetString(3);
                    user.Email = reader.GetString(4);
                    user.Active = reader.GetBoolean(5);

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

            return user;
        }

            // select user by userID
        public User SelectUserByUserID(int userID)
        {
            User user = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_user_by_userID";

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
                    reader.Read();

                    user = new User();

                    user.UserID = reader.GetInt32(0);
                    user.Username = reader.GetString(1);
                    user.DisplayName = reader.GetString(2);
                    user.ShortBio = reader.GetString(3);
                    user.Email = reader.GetString(4);
                    user.Active = reader.GetBoolean(5);

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

            return user;

        }

            // returns all roles
        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_roles";

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
                        roles.Add(reader.GetString(0));
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

            return roles;
        }

            // authenticate user for MVC
        public User AuthenticateUser(string email, string passwordHash)
        {
            User result = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_authenticate_user");
            cmd.Connection = conn;

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters for the procedure
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the values for the parameters
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the command is set up, we can execute it
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                if (1 == Convert.ToInt32(cmd.ExecuteScalar()))
                {
                    // if the command worked correctly, get a user
                    // object
                    result = SelectUserByEmail(email);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

            // user icon
        public string SelectUserIconByUserID(int userID)
        {
            string iconstring = "";

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_usericon_by_userid";

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
                        iconstring = reader.GetString(0);
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

            return iconstring;
        }

        ///////////////////////////////////////////////////////////////////
        ////////////////// UPDATE METHODS ////////////////////
        ///////////////////////////////////////////////////////////////////

            // update user bio
        public int UpdateUserBio(int userID, string shortBio)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_user_bio";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", userID);
            cmd.Parameters.AddWithValue("ShortBio", shortBio);

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

            // update password
        public int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_passwordHash";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);

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

        //////////////////////////////////////////////////////////////////////
        //////////////// DEACTIVATE METHODS //////////////////
        //////////////////////////////////////////////////////////////////////

            // add or delete user role for MVC
        public int InsertOrDeleteUserRole(int userId, string role, bool delete = false)
        {
            int rows = 0;

            string cmdText = delete ? "sp_delete_user_role" : "sp_insert_user_role";

            DBConnection connectionFactory = new DBConnection();

            var conn = connectionFactory.GetConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@RoleID", role);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

    }
}
