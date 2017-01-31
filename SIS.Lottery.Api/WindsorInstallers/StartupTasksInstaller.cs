using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SIS.Lottery.Api.StartupTasks;

namespace SIS.Lottery.Api.WindsorInstallers
{
    public class StartupTasksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IStartupTask>().WithServiceFirstInterface());
        }
    }
}