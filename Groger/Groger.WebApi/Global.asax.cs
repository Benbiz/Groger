using Groger.DTO;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Groger.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            AutoMapperConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
