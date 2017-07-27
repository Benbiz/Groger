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
using System.Web.Http.Description;

namespace Groger.WebApi.Controllers
{
    [RoutePrefix("api/clusters/{id:int}/Users")]
    public class ClusterUsersController : BaseApiController
    {
        public ClusterUsersController()
        {

        }

        public ClusterUsersController(IUnitOfWork uow)
            : base(uow)
        {
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult GetUsers(int id)
        {
            Cluster entity = UnitOfWork.ClusterRepository.GetByID(id);

            if (entity == null)
                return NotFound();
            else if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            return Ok(Mapper.Map<IEnumerable<UserDTO>>(entity.ApplicationUsers));
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult AddUsers(int id, UserDTO user)
        {
            Cluster entity = UnitOfWork.ClusterRepository.GetByID(id);

            if (entity == null)
                return NotFound();
            else if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == UserRecord.Id) == null)
                return Unauthorized();

            ApplicationUser u = UnitOfWork.AuthRepository.FindUserByName(user.UserName);
            if (u == null)
                return BadRequest(string.Format("No user correspond to username {0}", user.UserName));
            if (entity.ApplicationUsers.FirstOrDefault(x => x.Id == u.Id) != null)
                return BadRequest(string.Format("User {0} already link to this cluster", user.UserName));

            entity.ApplicationUsers.Add(u);

            UnitOfWork.ClusterRepository.Update(entity);
            UnitOfWork.Save();

            return Ok(Mapper.Map<IEnumerable<UserDTO>>(entity.ApplicationUsers));
        }
    }
}
