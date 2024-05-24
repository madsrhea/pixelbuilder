using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using DataAccessLayerFakes;
using DataAccessLayerInterfaces;
using LogicLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        UserManager userManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            userManager = new UserManager(new UserAccessorFake());
        }

        // checks to see if having the correct username and password will successfully log the user in
        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernameAndPassword()
        {
            // arrange, act, assert

            const string email = "stinkylad69@gmail.com";
            const string password = "newuser";
            int expectedResult = 99999;
            int actualResult = 0;

            User stinkyUser = userManager.LoginUser(email, password);
            actualResult = stinkyUser.UserID;

            Assert.AreEqual(expectedResult, actualResult);

        }

        // throws exception when username is incorrect
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithIncorrectUsername()
        {
            const string email = "fakeEmail@fake.email";
            const string password = "newuser";

            User fakeUser = userManager.LoginUser(email, password);

        }

        // throws exception when password is incorrect
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateUserFailsWithIncorrectPassword()
        {
            const string email = "stinkylad69@gmail.com"; 
            const string password = "wrongpassword";

            User fakeUser = userManager.LoginUser(email, password);

        }

        // checking to see if the password expected to go through comes back correctly
        [TestMethod]
        public void TestGetSHA256ReturnsCorrectHashValue()
        {
            const string source = "newuser";
            const string expectedResult = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            string result = "";

            result = userManager.HashSha256(source);

            Assert.AreEqual(expectedResult, result);
        }

        // throws error when password is null
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSHA256ThrowsArgumentNullExcepionForMissingInput()
        {
            const string source = null;

            userManager.HashSha256(source);
        }
        
        // throws error when password is an empty string
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSHA256ThrowsArgumentNullExcepionForEmptyString()
        {
            const string source = "";

            userManager.HashSha256(source);
        }

    }
}
