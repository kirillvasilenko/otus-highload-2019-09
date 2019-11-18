using System.Threading.Tasks;
using AuthService;
using AuthService.Users;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;

namespace SocialNetwork.AspNet.Services
{
    /// <summary>
    /// Resource owner password validator
    /// </summary>
    /// <seealso cref="IdentityServer4.Validation.IResourceOwnerPasswordValidator" />
    public class UserPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthUsersService authUsers;
        private readonly ISystemClock clock;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPasswordValidator"/> class.
        /// </summary>
        /// <param name="authUsers">The users.</param>
        /// <param name="clock">The clock.</param>
        public UserPasswordValidator(IAuthUsersService authUsers, ISystemClock clock)
        {
            this.authUsers = authUsers;
            this.clock = clock;
        }

        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await authUsers.ValidateCredentials(context.UserName, context.Password))
            {
                var user = await authUsers.FindByUsername(context.UserName);
                context.Result = new GrantValidationResult(
                    user.Id.ToString(), 
                    OidcConstants.AuthenticationMethods.Password, clock.UtcNow.UtcDateTime, 
                    user.GetClaims());
            }
        }
    }
}