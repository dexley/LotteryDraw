using System.Web.Http;
using SIS.Lottery.Api.StartupTasks;

namespace SIS.Lottery.Api.WindsorInstallers
{
    public class RouteConfigurationTask : IStartupTask
    {
        private readonly HttpConfiguration _configuration;

        public RouteConfigurationTask(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Run()
        {
            _configuration.Routes.MapHttpRoute(
                name: "ApiRoot",
                routeTemplate: "api",
                defaults: new { controller = "Home", action = "Get" }

                );
        }
    }
}