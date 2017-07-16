using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers
{
    public class GroceriesController : BaseApiController
    {
        public GroceriesController()
        {
        }

        public GroceriesController(IUnitOfWork unit)
            : base(unit)
        {
        }

        // GET: api/Grocery
        [SwaggerResponse(HttpStatusCode.OK, "Grocery list", typeof(GroceryDTO))]
        public IQueryable<GroceryDTO> GetGroceries()
        {
            var groceries = Mapper.Map<IEnumerable<GroceryDTO>>(UnitOfWork.GroceryRepository.Get());
            return groceries.AsQueryable();
        }

        // GET: api/Grocery/5
        [ResponseType(typeof(GroceryDTO))]
        public IHttpActionResult GetGrocery(int id)
        {
            var entity = UnitOfWork.GroceryRepository.GetByID(id);
            if (entity == null)
            {
                return NotFound();
            }
            GroceryDTO groceryDTO = Mapper.Map<GroceryDTO>(entity);
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

            UnitOfWork.GroceryRepository.Update(grocery);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UnitOfWork.GroceryRepository.GetByID(id) == null)
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

            UnitOfWork.GroceryRepository.Insert(grocery);
            UnitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = grocery.Id }, grocery);
        }

        // DELETE: api/Grocery/5
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult DeleteGroceryDTO(int id)
        {
            Grocery grocery = UnitOfWork.GroceryRepository.GetByID(id);
            if (grocery == null)
            {
                return NotFound();
            }

            UnitOfWork.GroceryRepository.Delete(id);
            UnitOfWork.Save();

            return Ok(grocery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}