using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Camarilla.RestApi;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Managers;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Camarilla.RestApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);
            WebApiConfig.Register(httpConfig);
            SwaggerConfig.Register(httpConfig);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(CamarillaContext.Create);
            app.CreatePerOwinContext<UserManager>(UserManager.Create);

            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here

        }
    }
}
