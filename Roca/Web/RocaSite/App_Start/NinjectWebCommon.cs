using Cno.Roca.BackEnd.AutoPlant.BL;
using Cno.Roca.BackEnd.Materials.BL.Repositories;
using Cno.Roca.BackEnd.Materials.BL.Services;
using Cno.Roca.BackEnd.Materials.EfDal;
using Cno.Roca.Web.RocaSite.Infrastructure;
using Cno.Roca.Web.RocaSite.Log;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Cno.Roca.Web.RocaSite.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Cno.Roca.Web.RocaSite.App_Start.NinjectWebCommon), "Stop")]

namespace Cno.Roca.Web.RocaSite.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IModelRepository>().To(typeof(CachedModelRepository));
            //kernel.Bind<IRocaUow>().To(typeof(RocaUow)).WhenInjectedInto(typeof(RoleProvider)).InTransientScope();
            kernel.Bind<IRocaUow>().To(typeof(RocaUow)).InRequestScope();
            kernel.Bind<IRocaService>().To(typeof(RocaService)).InRequestScope();
            //kernel.Bind<IRocaUow>().ToConstant(GetRocaUowMock().Object);
            kernel.Bind<ISessionManager>().To(typeof(SessionManager));
            kernel.Bind<ILogger>().To(typeof(NLogLogger));

            kernel.Bind<IAuthProvider>().To(typeof(FormsAuthProvider));
        }        
    }
}
