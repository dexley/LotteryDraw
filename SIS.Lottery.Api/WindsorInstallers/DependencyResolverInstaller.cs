using System.Web.Http.Dependencies;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SIS.Lottery.Api.WindsorConfiguration;

namespace SIS.Lottery.Api.WindsorInstallers
{
    public class DependencyResolverInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDependencyResolver>().ImplementedBy<WindsorDependencyResolver>());
        }
    }
}