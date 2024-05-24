using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IFollowerManager
    {
        // CREATE - INSERT
        int CreateFollow(int userID, int followingID);

        // READ - SELECT
        List<User> RetrieveAllUserFollowers(int userID);
        List<User> RetrieveAllUsersFollowing(int followingID);
        bool ConfirmFollow(int followingID, int userID);

        // UPDATE

        // DEACTIVATE
        int RemoveFollow(int userID, int followingID);

    }
}
