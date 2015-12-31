using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_Interception.Behaviors;
using Unity_Interception.Caching;
using Unity_Interception.Domain.Repositories;

namespace Unity_Interception
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<ICacheProvider, RunTimeCacheProvider>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<ICacheKeyGenerator, SimpleCacheKeyGenerator>(
                new ContainerControlledLifetimeManager());
            container.RegisterType<ICacheRegionNameGenerator, SimpleCacheRegionNameGenerator>(
                new ContainerControlledLifetimeManager());

            RegisterOneToOneImplementations(container);
            
            // need to re-register the following so we can change the LifeTime Manager
            container.RegisterType<ICustomerRepository, CustomerRepository>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<CachingBehavior>());



            var repository = container.Resolve<ICustomerRepository>();
            
        }
        
        private static void RegisterOneToOneImplementations(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies()
                    .Where(o => o.Namespace.StartsWith("Unity_Interception.Domain")),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled,
                (t) => new List<InjectionMember>()
                {
                     new Interceptor<InterfaceInterceptor>(),
                     new InterceptionBehavior<CachingBehavior>()
                });
        }
    }
}
