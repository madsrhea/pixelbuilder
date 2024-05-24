using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        private List<User> fakeUsers = new List<User>();
        private List<string> fakePasswordHashes = new List<string>();

        public UserAccessorFake()
        {
            fakeUsers.Add(new User()
            {
                UserID = 99999,
                Username = "StinkyLad",
                ShortBio = "Well, it's on the tin!",
                Email = "stinkylad69@gmail.com",
                Created = new DateTime(),
                Updated = DateTime.Now,
                Active = true,
                Role = "Creator"
            });


            fakeUsers.Add(new User()
            {
                UserID = 99998,
                Username = "forestFarie",
                ShortBio = "nature is beautiful ~\ninsta: @forest.farie.91\ntwitter: @runawaychild",
                Email = "allOfMyPaycheckGoesToEtsy@gmail.com",
                Created = new DateTime().AddYears(990),
                Updated = DateTime.Now,
                Active = true,
                Role = "Admin"
            });


            fakeUsers.Add(new User()
            {
                UserID = 99997,
                Username = "ALMIGHTYDAN",
                ShortBio = "RED-PILLED AND LOVING IT XD\nBEST FORTNITE STREAMER AND DON'T YOU FORGET IT!\nTWITCH & YOUTUBE: @ALMIGHTYPEPE",
                Email = "TopRedditUserEver@gmail.com",
                Created = new DateTime().AddYears(1012),
                Updated = DateTime.Now,
                Active = false, // he got banned for being a pest
                Role = "AccountUser"
            });

            fakePasswordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            fakePasswordHashes.Add("🤷‍");
            fakePasswordHashes.Add("😏");

        }


        //////////////////////////////////////////////////////
        ///////////// CREATE - INSERT METHODS ////////////////
        //////////////////////////////////////////////////////

            // incomplete -- testing to see if a new user can successfully be added
        public int InsertUser(User user)
        {
            throw new NotImplementedException();
        }



        //////////////////////////////////////////////////////
        ////////////// READ - SELECT METHODS /////////////////
        //////////////////////////////////////////////////////

            // testing to see if a user's email and password can be verified
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int num = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].Email == email && fakePasswordHashes[i] == passwordHash && fakeUsers[i].Active == true)
                {
                    num++;
                }
            }

            return num;
        }
            
            // testing to see if a user's role can be found in database by userID
        public string SelectRoleByUserID(int userID)
        {
            string role = "";

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UserID == userID)
                {
                    role = fakeUser.Role;
                    break;
                }
            }

            return role;
        }

            // testing to see if a user can be found by email
        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email == email)
                {
                    user = fakeUser;
                }
            }
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }

            // testing to see if a user can be found by userID
        public User SelectUserByUserID(int userID)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UserID == userID)
                {
                    user = fakeUser;
                }
            }

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }



        //////////////////////////////////////////////////////
        ////////////////// UPDATE METHODS ////////////////////
        //////////////////////////////////////////////////////

            // incomplete -- testing to see if a user's password can be successfully updated 
        public int UpdatePasswordHash(int userID, string passwordHash, string oldPasswordHash)
        {
            int num = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].UserID == userID)
                {
                    num++;
                }
            }

            return num;
        }

            // incomplete -- testing to see if a user's profile biography can be successfully updated
        public int UpdateUserBio(int userID, string shortBio)
        {
            int num = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].UserID == userID && fakeUsers[i].ShortBio == shortBio)
                {
                    num++;
                }
            }

            return num;
        }

        public List<User> SelectAllUsers()
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllRoles()
        {
            throw new NotImplementedException();
        }

        public User AuthenticateUser(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public int InsertOrDeleteUserRole(int userId, string role, bool delete = false)
        {
            throw new NotImplementedException();
        }

        public string SelectUserIconByUserID(int userID)
        {
            throw new NotImplementedException();
        }



        //////////////////////////////////////////////////////
        //////////////// DEACTIVATE METHODS //////////////////
        //////////////////////////////////////////////////////

    }
}
