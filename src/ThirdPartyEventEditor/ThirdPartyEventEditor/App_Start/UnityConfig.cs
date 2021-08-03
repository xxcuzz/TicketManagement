using System.Web.Mvc;
using ThirdPartyEventEditor.Models;
using ThirdPartyEventEditor.Models.Interfaces;
using Unity;
using Unity.Mvc5;

namespace ThirdPartyEventEditor.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IEventJsonService, EventJsonService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}