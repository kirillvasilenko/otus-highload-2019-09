using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsersService.Model;

namespace UsersService.AspNet.Controllers
{
    /// <summary>
    /// Users service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService service;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUsersService service, ILogger<UsersController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Get Users.
        /// </summary>
        /// <param name="skip">Pagination skip count</param>
        /// <param name="take">Pagination take count</param>
        /// <returns>User</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<User>> GetUsers(int skip, int take)
        {
            return await service.GetUsers(skip, take);
        }
        
        /// <summary>
        /// Get User.
        /// </summary>
        /// <param name="userId">Идентификатор</param>
        /// <returns>User</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="500"></response>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUser(long userId)
        {
            try
            {
                return await service.GetUser(userId);
            }
            catch (ItemNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}