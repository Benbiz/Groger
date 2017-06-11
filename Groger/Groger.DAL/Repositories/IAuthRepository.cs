using Groger.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Groger.DAL.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(User user);

        Task<IdentityUser> FindUser(string userName, string password);
    }
}
