using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using ODataService.Models;

namespace ODataService.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute(
                "ODataRoute",
                "odata",
                GetEdmModel());

            config.EnsureInitialized();
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.Namespace = "AirVinyl";
            builder.ContainerName = "AirVinylContainer";

            builder.EntitySet<Country>("Countries");
            builder.EntitySet<Person>("People");

            return builder.GetEdmModel();
        }
    }
}
