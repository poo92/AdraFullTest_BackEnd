using System.Web.Http;
using System.Web.Mvc;
using BusinessLayer.services;
using BusinessLayer.services.Interfaces;
using DataAccessLibrary.Repository;
using DataAccessLibrary.Repository.Interfaces;
using Microsoft.Practices.Unity;
using Unity;
using Unity.AspNet.Mvc;

namespace AdraDevTest
{
    public static class Bootstrapper
    {
        //public static void Initialise()
        //{
        //    var container = BuildUnityContainer();

        //    DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        //}

        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // register dependency resolver for WebAPI RC
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterType<IAccountBalanceService, AccountBalanceService>();
            container.RegisterType<IAccountBalanceRepo, AccountBalanceRepo>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserRepo, UserRepo>();
            return container;
        }
    }
}