using System.Reflection;
using Autofac;
using PlanMyTrip.Library.Services;
using Module = Autofac.Module;

namespace PlanMyTrip.Library
{
    public class PlanMyTripLibraryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            // For example,
            // builder.RegisterType<CharacterRepository>().As<ICharacterRepository>();

            builder.RegisterType<IntentRouterService>().As<IIntentRouterService>();

            // REGISTERING TYPES BY CONVENTION
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service")) // REGISTERING ALL SERVICES
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("IntentHandler")) // REGISTERING ALL HANDLERS
                .AsSelf();
        }
    }
}
