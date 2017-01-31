using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Xml;

namespace SIS.Lottery.Api.StartupTasks
{
    public interface IStartupTask
    {
        void Run();
    }
}