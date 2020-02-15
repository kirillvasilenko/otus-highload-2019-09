using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SocialNetwork.App;
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
            [FromQuery]
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
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(
            [FromQuery]
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

        
        [HttpGet("headers")]
        public ActionResult GetHeaders()
        {
            // Request method, scheme, and path
            var result = $"Request Method: {Request.Method}{Environment.NewLine}" +
                         $"Request Scheme: {Request.Scheme}{Environment.NewLine}" +
                         $"Request Path: {Request.Path}{Environment.NewLine}" +
                         $"Request Headers:{Environment.NewLine}" +
                         string.Join("", Request.Headers.Select(h => $"{h.Key}: {h.Value}{Environment.NewLine}")) +
                         Environment.NewLine +
                         $"Request RemoteIp: {HttpContext.Connection.RemoteIpAddress}";
            
            return Ok(result);
        }
        
    }
}