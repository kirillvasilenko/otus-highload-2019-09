using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialNetwork.App;
using SocialNetwork.App.Dtos;


namespace SocialNetwork.AspNet.Controllers
{
    
    /// <summary>
    /// Auth service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegistrationService service;
        private readonly IAuthService authService;

        public AuthController(IRegistrationService service)
        {
            this.service = service;
            this.authService = authService;
        }
        
        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="data">Data of new user.</param>
        /// <returns>Access token</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegistrationUserResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegistrationUserResult>> RegisterUser(RegisterUserData data)
        {
            var result = await service.RegisterUser(data);
            return Ok(result);
        }
    }
}