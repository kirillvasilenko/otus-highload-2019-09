using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.App;
using SocialNetwork.App.Dtos;
using SocialNetwork.AspNet.Utils;
using SocialNetwork.Model;

namespace SocialNetwork.AspNet.Controllers
{
    /// <summary>
    /// Account api
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService usersSvc;

        public AccountController(IUsersService usersSvc)
        {
            this.usersSvc = usersSvc;
        }
        
        /// <summary>
        /// Get current user's account.
        /// </summary>
        /// <returns>User</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetAccount()
        {
            return await usersSvc.GetUser(User.GetId());
        }
        
        /// <summary>
        /// Update current user's account.
        /// </summary>
        /// <param name="updateData"></param>
        /// <returns>Users count</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> UpdateAccount(
            [FromBody]
            UpdateUserData updateData)
        {
            return await usersSvc.UpdateUser(User.GetId(), updateData);
        }
    }
}