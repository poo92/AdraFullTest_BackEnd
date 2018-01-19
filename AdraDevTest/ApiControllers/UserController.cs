using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.services.Interfaces;
using EntityClasses;
using System.Web.Http.Cors;

namespace AdraDevTest.ApiControllers
{
    [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "*")]

    public class UserController : ApiController
    {
        private IUserService _UserService;   // create BL object
        public UserController(IUserService userService)
        {
            _UserService = userService;   // initialize BL object
        }

        // user login method
        [Route("api/User/Login")]
        [HttpPost]
        public int Login(User user)
        {
            string username = user.username;
            string password = user.password;

            return _UserService.Login(username, password);


        }

        //method to add user
        [Route("api/User/AddUser")]
        [HttpPost]
        public string AddUser(User user)
        {
            User userBAL = new User();
            userBAL.username = user.username;
            userBAL.password = user.password;
            userBAL.userType = user.userType;
            userBAL.fname = user.fname;
            userBAL.lname = user.lname;
            return _UserService.AddUser(userBAL);
        }

        [Route("api/User/GetAllUSers")]
        [HttpPost]
        public string[] GetAllUsers()
        {
            return _UserService.GetAllUsers();
        }

        [Route("api/User/DeleteUser")]
        [HttpPost]
        public string DeleteUser(User user)
        {
            return _UserService.DeleteUser(user);
        }
    }
}
