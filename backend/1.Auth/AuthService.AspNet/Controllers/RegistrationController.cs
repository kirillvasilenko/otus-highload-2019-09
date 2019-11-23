using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthService.Users;
using AuthService.Users.Dtos;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.AspNet.Controllers
{
    /// <summary>
    /// Users service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationUsersService service;

        public RegistrationController(IRegistrationUsersService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="data">Data of new user.</param>
        /// <returns>User</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthUser>> RegisterUser(RegisterUserData data)
        {
            return await service.RegisterUser(data);
        }
    }
}