using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor userAccessor = null;

        public UserManager()
        { 
            userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor ua)
        {
            userAccessor = ua;
        }

        public bool AddUser(User user)
        {
            bool result = false;

            try
            {
                result = (1 == userAccessor.InsertUser(user));
                    
            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }

        public string HashSha256(string source)
        {
            string result = "";

            if (source == "" || source == null)
            {
                throw new ArgumentNullException("Missing input");
            }

            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();
            result = result.ToLower();

                return result;
        }

        public User LoginUser(string email, string password)
        {
            User user = null;

            try
            {
                password = HashSha256(password);
                if (1 == userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password))
                {
                    user = userAccessor.SelectUserByEmail(email);
                    user.Role = userAccessor.SelectRoleByUserID(user.UserID);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception up)
            {
                throw new ApplicationException("Bad Email or Password", up); 
            }

            return user;
        }

        public bool ResetPassword(User user, string email, string password, string oldPassword)
        {
            bool success = false;
            password = HashSha256(password);
            oldPassword = HashSha256(oldPassword);

            if (1 == userAccessor.UpdatePasswordHash(user.UserID, password, oldPassword))
            {
                success = true;
            }

            return success;
        }

        public User UpdateBio(User user, string email, string shortBio)
        {
            User _user = user;

            if (1 == userAccessor.UpdateUserBio(user.UserID, shortBio))
            {
                _user.ShortBio = shortBio;
            }

            return _user;
        }

        public List<User> RetrieveAllUsers()
        { 
            List<User> users = new List<User>();

            try
            {
                users = userAccessor.SelectAllUsers();
            }
            catch (Exception up)
            {
                throw up;
            }

            return users;
        }

        public string GetUserRole(int userID)
        {
            string role = null;

            try
            {
                role = userAccessor.SelectRoleByUserID(userID);
            }
            catch (Exception)
            {
                throw;
            }

            return role;
        }

        public List<string> RetrieveAllRoles()
        {
            List<string> roles = new List<string>();

            try
            {
                roles = userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roles;
        }

        public bool FindUser(string email)
        {
            try
            {
                return userAccessor.SelectUserByEmail(email) != null;
            }
            catch (ApplicationException up)
            {
                if (up.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public User AuthenticateUser(string email, string passwordHash)
        {
            User result = null;

            var password = HashSha256(passwordHash);
            passwordHash = null;

            try
            {
                result = userAccessor.AuthenticateUser(email, password);
            }
            catch (ApplicationException)
            {
                // no need to worry / do anything
                // just means the user isnt logged in
            }
            catch (Exception up)
            {
                throw new ApplicationException("Login failed!", up);
            }

            return result;
        }

        public int RetreieveUserIdFromEmail(string email)
        {
            try
            {
                return userAccessor.SelectUserByEmail(email).UserID;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database Error", ex);
            }
        }

        public bool AddUserRole(int userId, string role)
        {
            bool result = false;
            try
            {
                result = (1 == userAccessor.InsertOrDeleteUserRole(userId, role));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not added!", ex);
            }
            return result;
        }

        public bool DeleteUserRole(int userId, string role)
        {
            bool result = false;
            try
            {
                result = (1 == userAccessor.InsertOrDeleteUserRole(userId, role, delete: true));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not removed!", ex);
            }
            return result;
        }

        public string RetrieveUserIconByUserID(int userID)
        {
            string iconstring = null;

            try
            {
                iconstring = userAccessor.SelectUserIconByUserID(userID);
            }
            catch (Exception)
            {
                throw;
            }

            return iconstring;
        }

        public User RetrieveUserByUserID(int userID)
        {
                User user = new User();

                try
                {
                    user = userAccessor.SelectUserByUserID(userID);
                }
                catch (Exception up)
                {
                    throw up;
                }

                return user;
            
        }
    }
}
