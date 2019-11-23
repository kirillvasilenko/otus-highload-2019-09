using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersService.Model;

namespace UsersService.AspNet.Controllers
{
    /// <summary>
    /// Users service
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get Users.
        /// </summary>
        /// <param name="skip">Pagination skip count</param>
        /// <param name="take">Pagination take count</param>
        /// <returns>User</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<User>> GetUsers(int skip, int take)
        {
            return await service.GetUsers(skip, take);
        }
        
        /// <summary>
        /// Get User.
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        /// <returns>User</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUser(long userId)
        {
            return await service.GetUser(userId);
        }
    }
}