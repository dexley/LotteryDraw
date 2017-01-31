using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MongoDB.Driver;
using SIS.Lottery.Api.Application;
using SIS.Lottery.Api.Infrastructure;
using SIS.Lottery.Api.Models;

namespace SIS.Lottery.Api.WindsorInstallers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRepository<LotteryDraw>>().ImplementedBy<LotteryRepository>().LifestyleTransient());

            container.Register(
                Component.For<IMongoDatabase>()
                    .UsingFactoryMethod(() => new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("Lottery"))
                    .LifestyleTransient());
        }
    }
}