using System.Web.Http;
using SampleWebApplication.Models;
using WebApi.Versioning;

namespace SampleWebApplication.Controllers
{
    public class ProductController : ApiController
    {
        [VersionedRoute( "api/{version}/product/{id}", "1.0" )]
        [VersionedRoute( "api/product/{id}", "1.0" )]
        public Product GetV1( int id )
        {
            return new Product()
            {
                Id = id,
                Name = "SampleProduct"
            };
        }

        public ProductV2 Get( int id )
        {
            return new ProductV2()
            {
                Id = id,
                Name = "SampleProduct",
                Price = 10
            };
        }
    }
}
