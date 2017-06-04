using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using Swashbuckle.Swagger.Annotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers
{
    public class GroceriesController : ApiController
    {
        private IGroceryRepository repository = new GroceryRepository(new GrogerContext());

        // GET: api/Grocery
        [SwaggerResponse(HttpStatusCode.OK, "Grocery list", typeof(GroceryDTO))]
        public IQueryable<GroceryDTO> GetGroceries()
        {
            var groceries = from g in repository.GetGroceries()
                            select new GroceryDTO()
                            {
                                Id = g.Id,
                                Name = g.Name,
                                Description = g.Description,
                                Quantity = g.Quantity
                            };
            return groceries.AsQueryable();
        }

        // GET: api/Grocery/5
        [ResponseType(typeof(GroceryDTO))]
        public IHttpActionResult GetGrocery(int id)
        {
            var entity = repository.GetGroceryById(id);
            if (entity == null)
            {
                return NotFound();
            }
            GroceryDTO groceryDTO = new GroceryDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Quantity = entity.Quantity
            };
            return Ok(groceryDTO);
        }

        // PUT: api/Grocery/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrocery(int id, Grocery grocery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grocery.Id)
            {
                return BadRequest();
            }

            repository.UpdateGrocery(grocery);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (repository.GetGroceryById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Grocery
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult PostGrocery(Grocery grocery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.InsertGrocery(grocery);
            repository.Save();

            return CreatedAtRoute("DefaultApi", new { id = grocery.Id }, grocery);
        }

        // DELETE: api/Grocery/5
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult DeleteGroceryDTO(int id)
        {
            Grocery grocery = repository.GetGroceryById(id);
            if (grocery == null)
            {
                return NotFound();
            }

            repository.DeleteGrocery(id);
            repository.Save();

            return Ok(grocery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}