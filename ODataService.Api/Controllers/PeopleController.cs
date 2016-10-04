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
    public class PeopleController : ODataController
    {
        private readonly ODataServiceContext _ctx;

        public PeopleController()
        {
            _ctx = new ODataServiceContext ();
        }

        public IHttpActionResult Get()
        {
            return Ok(_ctx.People);
        }

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

        public IHttpActionResult Post(Person person)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _ctx.People.Max(p => p.Id);
            person.Id = id + 1;
            _ctx.People.Add(person);
            _ctx.SaveChanges();
            return Created(person);
        }

        public IHttpActionResult Put([FromODataUri]int key, Person personToUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (key != personToUpdate.Id)
            {
                ModelState.AddModelError("Key", "Invalid identifiers");
                return BadRequest(ModelState);
            }

            var person = _ctx.People.FirstOrDefault(p => p.Id == key);
            if (person == null) return BadRequest();

            _ctx.Entry(person).CurrentValues.SetValues(personToUpdate);
            _ctx.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Patch([FromODataUri] int key, Delta<Person> patch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentPerson = _ctx.People.FirstOrDefault(p => p.Id == key);

            if (currentPerson == null) return NotFound();
            
            patch.Patch(currentPerson);
            _ctx.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var currentPerson = _ctx.People.FirstOrDefault(p => p.Id == key);
            if (currentPerson == null)
            {
                return NotFound();
            }

            _ctx.People.Remove(currentPerson);
            _ctx.SaveChanges();

            // return No Content
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            // dispose the context
            _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}
