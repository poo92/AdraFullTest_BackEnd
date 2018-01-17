using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.services.Interfaces;
using DataAccessLibrary;
using DataAccessLibrary.Repository.Interfaces;
using EntityClasses;

namespace BusinessLayer.services
{
    public class UserService : IUserService
    {
        // create userRepo object
        private IUserRepo _UserRepo;
        public UserService(IUserRepo userRepo)
        {
            _UserRepo = userRepo; // Initialize userRepo object
        }


        //generate salt string to hash password 
        private static string getSalt()
        {
            var random = new RNGCryptoServiceProvider();
            int max_length = 32;             // Maximum length of salt            
            byte[] salt = new byte[max_length]; // Empty salt array            
            random.GetNonZeroBytes(salt);   // Build the random bytes            
            return Convert.ToBase64String(salt);    // Return the string encoded salt
        }

        // hash password method
        private string HashPassword(string password, string salt)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            string saltedPassword = password + salt;
            byte[] saltedPasswordBytes = Encoding.ASCII.GetBytes(saltedPassword);
            byte[] hashValue = mySHA256.ComputeHash(saltedPasswordBytes);

            return Convert.ToBase64String(hashValue);
        }

        // get user by username method
        private User GetUser(string username)
        {

            User userBAL = new User();

            // repository method to get user by username
            user userDAL = _UserRepo.ViewUser(username);

            // if user exsists in the db
            if (userDAL != null)
            {
                userBAL.username = userDAL.username;
                userBAL.password = userDAL.password;
                userBAL.userType = (int)userDAL.userType;
                userBAL.fname = userDAL.fname;
                userBAL.lname = userDAL.lname;
                userBAL.salt = userDAL.salt;

            }

            return userBAL;
        }

        // Add user method
        public string AddUser(User userBAL)
        {
            string result = "";

            // check if user is already in the database
            User userExists = GetUser(userBAL.username);

            // if user exists
            if (userExists.username != null)
            {
                result = "This username is already in use. Please enter another username.";
            }
            else
            {
                // if if user doesn't exist                
                // hashing the password
                string salt = getSalt();
                string password = HashPassword(userBAL.password, salt);

                // set values of DAL object
                user userDAL = new user();
                userDAL.username = userBAL.username;
                userDAL.password = password;
                userDAL.userType = userBAL.userType;
                userDAL.fname = userBAL.fname;
                userDAL.lname = userBAL.lname;
                userDAL.salt = salt;

                // call DAL method
                result = _UserRepo.AddUser(userDAL);
            }

            return result;
        }

        // method to view user
        public User ViewUser(string username)
        {
            User userBAL = GetUser(username);

            if (userBAL != null)
            {
                // need to hide password and the salt
                userBAL.password = "";
                userBAL.salt = "";

            }

            return userBAL;
        }


        // Login method
        public int Login(string username, string password)
        {
            int userType = 0;

            // to check if user is the db 
            User userExists = GetUser(username);

            // if user exists
            if (userExists.username != null)
            {
                // user's hashed password from db
                string dbPassword = userExists.password;
                // salt string from the db
                string salt = userExists.salt;

                // hash the password provided by the user along with the salt string
                string hashedPassword = HashPassword(password, salt);

                // if two passwords match
                if (dbPassword == hashedPassword)
                {
                    userType = userExists.userType;
                }
            }

            return userType;
        }

        public string DeleteUser(User user)
        {

            return _UserRepo.DeleteUser(user.username);
        }

        public string[] GetAllUsers()
        {
            List<user> usersDAL = _UserRepo.GetAllUsers();

            string[] userBAL = new string[usersDAL.Count];

            if (usersDAL != null)
            {
                for (int i = 0; i < usersDAL.Count; i++)
                {
                    user userDALObj = usersDAL[i];
                    userBAL[i] = userDALObj.username;
                }

            }

            return userBAL;
        }
    }
}
