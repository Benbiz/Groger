﻿using AutoMapper;
using Groger.DAL;
using Groger.DTO;
using Groger.Entity;
using System.Collections.Generic;
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
        [Route("search")]
        public IHttpActionResult SearchCategories([FromUri] CategoryDTO query)
        {
            var cat = UnitOfWork.CategoryRepository.Get(x => x.Name.Contains(query.Name) || x.Description.Contains(query.Description));

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

            return Ok(dto);
        }
    }
}
