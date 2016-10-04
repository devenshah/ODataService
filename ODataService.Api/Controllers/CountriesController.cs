using System.Linq;
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

        public IHttpActionResult Get(int key)
        {
            var country = _ctx.Countries.FirstOrDefault(c => c.Id == key);

            if (country == null) return NotFound();

            return Ok(country);
        }
        protected override void Dispose(bool disposing)
        {
            // dispose the context
            _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}
