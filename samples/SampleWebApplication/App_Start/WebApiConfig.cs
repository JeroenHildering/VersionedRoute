using System.Web.Http;
using WebApi.Versioning;

namespace SampleWebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Allow version to be part of the resource url
            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "api/{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { version = new VersionConstraint() } // Add version contraint here to force resolving only supported versions
            );

            // Default route template
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { version = new VersionConstraint() } // Add version contraint here to force resolving only supported versions
            );
        }
    }
}
