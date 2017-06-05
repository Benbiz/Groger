using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
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
        private IUnitOfWork unitOfWork;

        public GroceriesController()
        {
            unitOfWork = new UnitOfWork();
        }

        public GroceriesController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }

        // GET: api/Grocery
        [SwaggerResponse(HttpStatusCode.OK, "Grocery list", typeof(GroceryDTO))]
        public IQueryable<GroceryDTO> GetGroceries()
        {
            var groceries = Mapper.Map<IEnumerable<GroceryDTO>>(unitOfWork.GroceryRepository.Get());
            return groceries.AsQueryable();
        }

        // GET: api/Grocery/5
        [ResponseType(typeof(GroceryDTO))]
        public IHttpActionResult GetGrocery(int id)
        {
            var entity = unitOfWork.GroceryRepository.GetByID(id);
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

            unitOfWork.GroceryRepository.Update(grocery);

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.GroceryRepository.GetByID(id) == null)
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

            unitOfWork.GroceryRepository.Insert(grocery);
            unitOfWork.Save();

            return CreatedAtRoute("DefaultApi", new { id = grocery.Id }, grocery);
        }

        // DELETE: api/Grocery/5
        [ResponseType(typeof(Grocery))]
        public IHttpActionResult DeleteGroceryDTO(int id)
        {
            Grocery grocery = unitOfWork.GroceryRepository.GetByID(id);
            if (grocery == null)
            {
                return NotFound();
            }

            unitOfWork.GroceryRepository.Delete(id);
            unitOfWork.Save();

            return Ok(grocery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}