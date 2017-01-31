using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;

namespace SIS.Lottery.Api.StartupTasks
{
    public class HttpConfigurationTask : IStartupTask
    {
        private readonly IDependencyResolver _dependencyResolver;
        private readonly HttpConfiguration _configuration;

        public HttpConfigurationTask(IDependencyResolver dependencyResolver, HttpConfiguration configuration)
        {
            _dependencyResolver = dependencyResolver;
            _configuration = configuration;
        }

        public void Run()
        {
            _configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            //url append: swagger/ui/index
            _configuration.EnableSwagger(c => c.SingleApiVersion("v1", "Lottery API")).EnableSwaggerUi();
            _configuration.DependencyResolver = _dependencyResolver;

            _configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            _configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            _configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            _configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

        }
    }
}