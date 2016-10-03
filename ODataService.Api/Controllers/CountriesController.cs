using System.Web.Http;
using System.Web.OData;
using ODataService.DataAccess;

namespace ODataService.Api.Controllers
{
    public class CountriesController : ODataController
    {
        private ODataServiceContext _ctx = new ODataServiceContext();

        public IHttpActionResult Get()
        {
            return Ok(_ctx.Countries);
        }
    }
}
