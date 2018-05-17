using AutoMapper;
using Groger.DAL;
using Groger.DTO.Grocery;
using System.Collections.Generic;
using System.Web.Http;

namespace Groger.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/groceries")]
    public class GroceriesController : BaseApiController
    {
        public GroceriesController()
        {
        }

        public GroceriesController(IUnitOfWork unit)
            : base(unit)
        {
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult SearchGroceries([FromUri] SearchGroceryDTO query)
        {
            var groceries = UnitOfWork.GroceryRepository.Get(
                x => x.Name.Contains(query.Name)
                || x.Description.Contains(query.Description)
                || x.Category.Name.Contains(query.Categorie));

            return Ok(Mapper.Map<IEnumerable<GrocerySearchResultDTO>>(groceries));
        }
    }
}
