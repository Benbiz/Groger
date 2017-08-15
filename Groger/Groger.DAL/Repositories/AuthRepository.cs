using Groger.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Groger.DAL.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private GrogerContext context;
        private UserManager<ApplicationUser> userManager;


        public AuthRepository()
        {
            this.context = new GrogerContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));
        }

        public AuthRepository(GrogerContext context)
        {
            this.context = context;
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));
        }

        public ApplicationUser FindUserByName(string userName)
        {
            ApplicationUser user = userManager.FindByName(userName);

            return user;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            ApplicationUser newuser = new ApplicationUser
            {
                UserName = user.UserName
            };

            var result = await userManager.CreateAsync(newuser, user.Password);
            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                    userManager.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
