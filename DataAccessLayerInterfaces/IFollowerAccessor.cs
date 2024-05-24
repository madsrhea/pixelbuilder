using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IFollowerAccessor
    {
        // CREATE - INSERT
        int InsertFollow(int userID, int followingID);

        // READ - SELECT
        List<User> SelectAllUserFollowers(int userID);
        List<User> SelectAllUsersFollowing(int followingID);
        int AuthenticateFollow(int followingID, int userID);

        // UPDATE

        // DEACTIVATE
        int DeleteFollow(int userID, int followingID);
    }
}
