using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SocialNetwork.App;
using SocialNetwork.App.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.AspNet.Controllers
{
    /// <summary>
    /// Users service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUsersService usersSvc;
        
        public UsersController(IUsersService usersSvc)
        {
            this.usersSvc = usersSvc;
        }
        
        /// <summary>
        /// Get an user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetUser(
            [BindRequired]
            long userId)
        {
            return await usersSvc.GetUser(userId);
        }
        
        /// <summary>
        /// Get users count.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Users count</returns>
        [HttpGet("count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetUsersCount(
            GetUsersQuery query)
        {
            return await usersSvc.GetUsersCount(query);
        }
    }
}