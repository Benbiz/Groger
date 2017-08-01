using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Groger.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/categories")]
    public class CategoriesController : BaseApiController
    {
        public CategoriesController()
        {
        }

        public CategoriesController(IUnitOfWork uow)
            : base(uow)
        {
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCategories()
        {
            IQueryable<Cluster> query = UserRecord.Clusters.AsQueryable();

            IQueryable<Grocery> gro = query.SelectMany(x => x.Groceries);

            var cat = gro.Select(x => x.Category).GroupBy(x => x);

            List<GetCategoryDTO> cats = new List<GetCategoryDTO>();

            foreach (IGrouping<Category, Category> group in cat)
            {
                var dto = Mapper.Map<GetCategoryDTO>(group.Key);
                dto.Quantity = group.Count();
                cats.Add(dto);
            }

            return Ok(cats);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCategory")]
        public IHttpActionResult GetGrocery(int id)
        {
            IQueryable<Cluster> query = UserRecord.Clusters.AsQueryable();

            IQueryable<Grocery> gro = query.SelectMany(x => x.Groceries);

            var cats = gro.Select(x => x.Category).GroupBy(x => x);

            var cat = cats.FirstOrDefault(x => x.Key.Id == id);
            if (cat == null)
                return NotFound();

            var dto = Mapper.Map<GetCategoryDTO>(cat.Key);
            dto.Quantity = cat.Count();

            return Ok(dto);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchCategories([FromUri] string query)
        {
            var cat = UnitOfWork.CategoryRepository.Get(x => (x.Name.Contains(query) || x.Description.Contains(query)));

            return Ok(Mapper.Map<IEnumerable<CategoryDTO>>(cat));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateCategories([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category entity = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };

            UnitOfWork.CategoryRepository.Insert(entity);
            UnitOfWork.Save();

            var dto = Mapper.Map<GetCategoryDTO>(entity);
            dto.Quantity = 0;

            return CreatedAtRoute("GetCategory", new { id = entity.Id }, dto);
        }
    }
}
