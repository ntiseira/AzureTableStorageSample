using CommonServiceLocator;
 using ChatRoom.Cloud;
using ChatRoom.Cloud.Interfaces;
using ChatRoom.Cloud.Repository;
using ChatRoom.Services;
using ChatRoom.Services.Interfaces;
using System;
using System.Web.Http;
using Unity;
using Unity.AspNet.Mvc;
using Unity.AspNet.WebApi;
using Unity.Lifetime;
using Unity.ServiceLocation;

namespace ChatRoom.Test
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Resolves dependency injection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            container.RegisterType<IChatRoomService, ChatRoomService>();

            


            container.RegisterType(typeof(IQueueStorageRepository ), typeof(QueueStorageRepository));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(container);
        }
    }
}