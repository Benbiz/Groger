using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Groger.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Groger.DAL.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private AuthContext context;
        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            this.context = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this.context));
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            IdentityUser newuser = new IdentityUser
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
