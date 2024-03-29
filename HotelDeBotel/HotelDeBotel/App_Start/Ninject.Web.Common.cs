[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HotelDeBotel.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(HotelDeBotel.App_Start.NinjectWebCommon), "Stop")]

namespace HotelDeBotel.App_Start
{
    using System;
    using System.Web;
    using HotelDeBotel.Models;
    using HotelDeBotel.Models.Repositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

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
            kernel.Bind<IRoomRepository>().To<DummyRoomRepository>().InSingletonScope();
            kernel.Bind<IReservationRepository>().To<DummyReservationRepository>().InSingletonScope();
            kernel.Bind<IGuestRepository>().To<DummyGuestRepository>().InSingletonScope();
            kernel.Bind<IDiscountRepository>().To<DummyDiscountRepository>().InSingletonScope();
        }
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        //private static void RegisterServices(IKernel kernel)
        //{
        //    kernel.Bind<BotelContext>().ToSelf().InSingletonScope();
        //    kernel.Bind<IRoomRepository>().To<RoomRepository>().InSingletonScope();
        //    kernel.Bind<IReservationRepository>().To<ReservationRepository>().InSingletonScope();
        //    kernel.Bind<IGuestRepository>().To<GuestRepository>().InSingletonScope();
        //    kernel.Bind<IDiscountRepository>().To<DiscountRepository>().InSingletonScope();
        //}
    }
}