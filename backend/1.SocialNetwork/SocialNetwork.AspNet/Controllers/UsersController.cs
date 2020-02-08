using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SocialNetwork.App;
using SocialNetwork.App.Dtos;
using SocialNetwork.AspNet.Utils;
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

        /// <summary>
        /// Get users.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Users count</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(
            GetUsersQuery query,
            [DefaultValue(0)]
            [Range(0, int.MaxValue)]
            int skip,
            [DefaultValue(50)]
            [Range(1, 100)]
            int take)
        {
            var result = await usersSvc.GetUsers(query, skip, take);
            return Ok(result);
        }

        /// <summary>
        /// Update an user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="updateData"></param>
        /// <returns>Users count</returns>
        [HttpPut("{userId}")]
        [Authorize]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> UpdateUser(
            [BindRequired]
            long userId,
            [FromBody]
            UpdateUserData updateData)
        {
            if (User.GetId() != userId)
            {
                return Forbid();
            }
            return await usersSvc.UpdateUser(userId, updateData);
        }
        
        [HttpGet("test")]
        public async Task<ActionResult> Dich()
        {
            return Ok(HttpContext.Request.Headers.Select(x => $"{x.Key}:{x.Value}").ToList());
        }
        
    }
}