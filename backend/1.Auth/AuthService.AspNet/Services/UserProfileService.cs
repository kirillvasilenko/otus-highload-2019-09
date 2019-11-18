using System.Linq;
using System.Threading.Tasks;
using AuthService;
using AuthService.Users;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace SocialNetwork.AspNet.Services
{
    /// <summary>
    /// Profile service for users
    /// </summary>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class UserProfileService : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;
        
        /// <summary>
        /// The users
        /// </summary>
        private readonly IAuthUsersService authUsers;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="authUsers">The users.</param>
        /// <param name="logger">The logger.</param>
        public UserProfileService(IAuthUsersService authUsers, ILogger<UserProfileService> logger)
        {
            this.authUsers = authUsers;
            this.logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(logger);

            if (context.RequestedClaimTypes.Any())
            {
                var user = await authUsers.FindById(long.Parse(context.Subject.GetSubjectId()));
                if (user != null)
                {
                    context.AddRequestedClaims(user.GetClaims());
                }
            }

            context.LogIssuedClaims(logger);
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = await authUsers.FindById(long.Parse(context.Subject.GetSubjectId()));
            context.IsActive = user?.IsActive == true;
        }
    }
}