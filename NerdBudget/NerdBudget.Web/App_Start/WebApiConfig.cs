using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NerdBudget.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
         JsonSerializerSettings jss =   config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings;
            
            jss.ContractResolver = new CamelCasePropertyNamesContractResolver();

            jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; 

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
