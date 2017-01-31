using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SIS.Lottery.Api.StartupTasks;
using SIS.Lottery.Api.WindsorConfiguration;
using SIS.Lottery.Api.WindsorInstallers;

namespace SIS.Lottery.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new WindsorContainerFactory().CreateContainer();

            var tasks = container.ResolveAll<IStartupTask>();
            tasks.ForEach(t => t.Run());
        }
    }
}
