using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {

        // CREATE - INSERT
        bool AddUser(User user);
        bool AddUserRole(int userId, string role);

        // READ - SELECT
        List<User> RetrieveAllUsers();
        User LoginUser(string email, string password);
        string GetUserRole(int userID);
        string HashSha256(string source);
        List<string> RetrieveAllRoles();
        bool FindUser(string email);
        User AuthenticateUser(string email, string passwordHash);
        int RetreieveUserIdFromEmail(string email);
        string RetrieveUserIconByUserID(int userID);
        User RetrieveUserByUserID(int userID);


        // UPDATE
        User UpdateBio(User user, string email, string shortBio);
        bool ResetPassword(User user, string email, string password, string oldPassword);


        // DELETE - DEACTIVATE
        bool DeleteUserRole(int userId, string role);

    }
}
