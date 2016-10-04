using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using ODataService.Api.Helpers;
using ODataService.DataAccess;
using ODataService.Models;

namespace ODataService.Api.Controllers
{
    public class PersonController : ODataController
    {
        private readonly ODataServiceContext _ctx;

        public PersonController()
        {
            _ctx = new ODataServiceContext ();
        }

        [HttpGet]
        [ODataRoute("People")]
        public IHttpActionResult Get()
        {
            return Ok(_ctx.People);
        }

        [HttpGet]
        [ODataRoute("People({key})")]
        public IHttpActionResult Get(int key)
        {
            var person = _ctx.People.FirstOrDefault(p => p.Id == key);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpGet]
        [ODataRoute("People({key})/FirstName")]
        [ODataRoute("People({key})/LastName")]
        public IHttpActionResult GetProperty(int key)
        {
            var field = Url.Request.RequestUri.Segments.Last();
            var person =
                _ctx.People
                    .FirstOrDefault(p => p.Id == key);            

            if (person == null) return NotFound();

            if (!person.HasProperty(field)) return NotFound();

            var propertyValue = person.GetValue(field);

            if (propertyValue == null) return StatusCode(HttpStatusCode.NoContent);

            return this.CreateOKHttpActionResult(propertyValue);
        }

        [HttpPost]
        [ODataRoute("People")]
        public IHttpActionResult Post(Person person)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _ctx.People.Max(p => p.Id);
            person.Id = id + 1;
            _ctx.People.Add(person);
            _ctx.SaveChanges();
            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            // dispose the context
            _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}
