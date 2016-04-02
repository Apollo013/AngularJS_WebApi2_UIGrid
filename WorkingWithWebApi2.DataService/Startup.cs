using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(WorkingWithWebApi2.DataService.Startup))]

namespace WorkingWithWebApi2.DataService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Ninject
            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());

            // Configure API routes & message format
            WebApiConfig.Register(config);

            // Wire up ASP.NET Web API to our Owin server request pipeline
            app.UseWebApi(config);
        }
    }
}
