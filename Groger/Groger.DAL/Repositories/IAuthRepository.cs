using Groger.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Groger.DAL.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(User user);

        Task<ApplicationUser> FindUser(string userName, string password);
    }
}
