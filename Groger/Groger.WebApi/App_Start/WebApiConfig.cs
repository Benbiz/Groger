using Groger.DTO;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Groger.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services de l'Web API 

            // Itinéraires de l'Web API 
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            AutoMapperConfiguration.Configure();
        }
    }
}
