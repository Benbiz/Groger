using Groger.DAL;
using Groger.DAL.Repositories;
using Groger.Entity;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace Groger.WebApi.Controllers
{
    [RoutePrefix("api/Users")]
    [Authorize]
    public class UsersController : BaseApiController
    {
        public UsersController(IUnitOfWork unit)
            : base(unit)
        {
        }

        public UsersController()
        {
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UnitOfWork.AuthRepository.RegisterUser(user);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok();
        }

        #region Dispose Implementation
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                UnitOfWork.AuthRepository.Dispose();
            base.Dispose(disposing);
        }


        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach(string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                
                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }
            return null;
        }
        #endregion
    }
}
