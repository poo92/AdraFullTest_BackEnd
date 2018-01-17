using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Repository.Interfaces;

namespace DataAccessLibrary.Repository
{
    public class UserRepo :IUserRepo
    {
        private adradb DbContext;
        public UserRepo()
        {
            DbContext = new adradb();
        }

        // add user method
        public string AddUser(user user)
        {
            // add new user to the db
            DbContext.users.Add(user);
            int result = DbContext.SaveChanges();

            if (result != 0)
            {
                return "User Added succesfully";
            }
            else
            {
                return "An error occured.Please try again";
            }
        }

        // method to get user by username
        public user ViewUser(string username)
        {
            user user = DbContext.users.Where(o => o.username == username).FirstOrDefault();
            return user;
        }

        public string DeleteUser(string username)
        {
            user userToDelete = DbContext.users.Where(o => o.username == username).FirstOrDefault();
            DbContext.Entry(userToDelete).State = System.Data.Entity.EntityState.Deleted;
            int result = DbContext.SaveChanges();

            if (result != 0)
            {
                return "deleted successfully";
            }
            else
            {
                return "Error occured while deleting";
            }
        }

        public List<user> GetAllUsers()
        {
            List<user> result = DbContext.users.ToList();
            return result;
        }
    }
}
