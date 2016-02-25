using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using SampleWebApplication.Models;
using WebApi.Versioning;

namespace SampleWebApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Add current header version to all responses
            GlobalConfiguration.Configuration.Filters.Add( new VersionAttribute() );

            // Only allow json and xml as output
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add( new JsonMediaTypeFormatter() );
            GlobalConfiguration.Configuration.Formatters.Add( new XmlMediaTypeFormatter() );

            // Add custom media type formatters for supported versions
            foreach ( var supportedVersion in ConfigurationManager.AppSettings["SupportedApiVersions"].Split( ',' ).Select( x => x.Trim() ) )
            {
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue( string.Format( "application/vnd.webapiversioning-v{0}+json", supportedVersion ) ) );

                GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Add(
                    new MediaTypeHeaderValue( string.Format( "application/vnd.webapiversioning-v{0}+xml", supportedVersion ) ) );
            }

            // Add typed formatters
            GlobalConfiguration.Configuration.Formatters.Add(
                    new TypedJsonMediaTypeFormatter( typeof( Product ),
                        new MediaTypeHeaderValue( "application/vnd.vendor.product-v1.0+json" ) ) );
            GlobalConfiguration.Configuration.Formatters.Add(
                    new TypedJsonMediaTypeFormatter( typeof( ProductV2 ),
                        new MediaTypeHeaderValue( "application/vnd.vendor.product-v2.0+json" ) ) );

            GlobalConfiguration.Configuration.Formatters.Add(
                new TypedXmlMediaTypeFormatter( typeof( Product ),
                    new MediaTypeHeaderValue( "application/vnd.vendor.product-v1.0+xml" ) ) );
            GlobalConfiguration.Configuration.Formatters.Add(
                new TypedXmlMediaTypeFormatter( typeof( ProductV2 ),
                    new MediaTypeHeaderValue( "application/vnd.vendor.product-v2.0+xml" ) ) );
        }
    }
}
