using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SIS.Lottery.Api.WindsorInstallers
{
    public class HttpConfigurationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<HttpConfiguration>().Instance(GlobalConfiguration.Configuration));
        }
    }
}