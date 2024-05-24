using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class FollowerManager : IFollowerManager
    {
        private IFollowerAccessor _followerAccessor = null;

        public FollowerManager()
        {
            _followerAccessor = new DataAccessLayer.FollowerAccessor();
        }

        public FollowerManager(IFollowerAccessor followerAccessor)
        {
            _followerAccessor = followerAccessor;
        }

        // CREATE - INSERT
        public int CreateFollow(int userID, int followingID)
        {
            int result = 0;

            try
            {
                result = _followerAccessor.InsertFollow(userID, followingID);
            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }

        // READ - SELECT
        public List<User> RetrieveAllUserFollowers(int userID)
        {
            List<User> userList = null;

            try
            {
                userList = _followerAccessor.SelectAllUserFollowers(userID);
            }
            catch (Exception up)
            {

                throw up;
            }

            return userList;
        }

        public List<User> RetrieveAllUsersFollowing(int followingID)
        {
            List<User> userList = null;

            try
            {
                userList = _followerAccessor.SelectAllUsersFollowing(followingID);
            }
            catch (Exception up)
            {

                throw up;
            }

            return userList;
        }

        // UPDATE

        // DELETE - DEACTIVATE
        public int RemoveFollow(int userID, int followingID)
        {
            int result = 0;

            try
            {
                result = _followerAccessor.DeleteFollow(userID, followingID);
            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }

        public bool ConfirmFollow(int followingID, int userID)
        {
            bool result = false;

            try
            {
                result = (1 == _followerAccessor.AuthenticateFollow(followingID, userID));

            }
            catch (Exception up)
            {
                throw up;
            }

            return result;
        }
    }
}
