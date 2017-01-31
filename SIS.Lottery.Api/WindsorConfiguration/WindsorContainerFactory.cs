using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using SIS.Lottery.Api.WindsorInstallers;

namespace SIS.Lottery.Api.WindsorConfiguration
{
    public class WindsorContainerFactory
    {
        public IWindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            return container;
        }
    }
}