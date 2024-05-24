using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface IUserAccessor
    {
            // CREATE - INSERT        
        int InsertUser(User user);

            // READ - SELECT
        List<User> SelectAllUsers(); 
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        User SelectUserByEmail(string email);
        User SelectUserByUserID(int userID);
        string SelectRoleByUserID(int userID);
        string SelectUserIconByUserID(int userID);
        List<string> SelectAllRoles();
        User AuthenticateUser(string email, string passwordHash);

            // UPDATE
        int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash);
        int UpdateUserBio(int userID, string shortBio);

            // DELETE - DEACTIVATE
        int InsertOrDeleteUserRole(int userId, string role, bool delete = false);

    }
}
