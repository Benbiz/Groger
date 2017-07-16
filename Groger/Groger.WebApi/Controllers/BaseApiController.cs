using Groger.DAL;
using Groger.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Groger.WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private IUnitOfWork unitOfWork;
        public IUnitOfWork UnitOfWork { get { return unitOfWork; } }


        public BaseApiController(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public BaseApiController()
        {
            unitOfWork = new UnitOfWork();
        }

        private ApplicationUser _user;
        public ApplicationUser UserRecord
        {
            get
            {
                if (_user == null)
                    _user = unitOfWork.AuthRepository.FindUserByName(User.Identity.Name);
                return _user;
            }
        }
    }
}
