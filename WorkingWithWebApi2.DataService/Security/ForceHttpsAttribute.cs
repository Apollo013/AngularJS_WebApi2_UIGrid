using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WorkingWithWebApi2.DataService.Security
{
    /// <summary>
    /// Class that forces request calls to be made over the HTTPS scheme
    /// </summary>    
    public class ForceHttpsAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Checks to make sure that POST, PUT, DELETE, etc. calls are made over HTTPS (Only GET requests are permitted over HTTP).
        /// If not, a uri is generated and injected into the response location header and returned to the client, forcing it to remake the call using the HTTPS scheme.
        /// </summary>
        /// <param name="actionContext">Object that contains Request & Response objects</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "Https is required";

                if (request.Method.Method == "GET")
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                    UriBuilder httpsNewUri = new UriBuilder(request.RequestUri);
                    httpsNewUri.Scheme = Uri.UriSchemeHttps;
                    httpsNewUri.Port = 443;

                    actionContext.Response.Headers.Location = httpsNewUri.Uri;
                }
                else
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }

            }
        }
    }
}
