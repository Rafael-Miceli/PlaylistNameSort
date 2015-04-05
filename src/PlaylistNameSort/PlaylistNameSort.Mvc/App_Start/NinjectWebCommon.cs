[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PlaylistNameSort.Mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PlaylistNameSort.Mvc.App_Start.NinjectWebCommon), "Stop")]

namespace PlaylistNameSort.Mvc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using PlaylistNameSort.Domain.Models;
    using System.Configuration;
    using System.Web.Configuration;
    using PlaylistNameSort.Domain;
    using PlaylistNameSort.Domain.Interfaces;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        public static void RegisterRootUrl(string rootUrl)
        {

        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //It would be great to get these values from some confi, but cannot access web.config from here
            string uriCallback = "http://playlistnamesort.azurewebsites.net/Home/GenerateNameSortList";
            string clientId = "c2b415ceb2694cb29b34088a69816aea";

            kernel.Bind(typeof(ISpotifyApi)).To(typeof(SpotifyApi));
            kernel.Bind(typeof(SpotifyAuthViewModel)).To(typeof(SpotifyAuthViewModel))
                .WithConstructorArgument("clientId", clientId)
                .WithConstructorArgument("redirectUri", uriCallback)
                .WithConstructorArgument("state", "")
                .WithConstructorArgument("scope", Scope.PLAYLIST_MODIFY_PRIVATE);
        }        
    }
}
