using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using WorkingWithWebApi2.DataService.Helpers.ActionFilterAttributes;
using WorkingWithWebApi2.DataService.Security;

namespace WorkingWithWebApi2.DataService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Format messages in Json
            ConfigureMessageFormat(config);
            
            // CORS
            ConfigureCORS(config);

            // Force HTTPs
#if !DEBUG
            config.Filters.Add(new ForceHttpsAttribute());
#endif

            // Action Filters
            config.Filters.Add(new ValidateModelAttribute());

        }

        /// <summary>
        /// Configure message format sent to client (Json)
        /// </summary>
        /// <param name="config"></param>
        private static void ConfigureMessageFormat(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();   // Use camel Case
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;              // Ignore circular references
        }

        /// <summary>
        /// CORS support configuration
        /// </summary>
        /// <param name="config"></param>
        private static void ConfigureCORS(HttpConfiguration config)
        {
            EnableCorsAttribute CorsAttribute = new EnableCorsAttribute("http://angularuigrid.azurewebsites.net", "*", "GET,POST,DELETE,PUT");
            config.EnableCors(CorsAttribute);
        }
    }
}
