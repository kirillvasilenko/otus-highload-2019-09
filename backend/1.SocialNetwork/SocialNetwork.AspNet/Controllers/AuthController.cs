using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialNetwork.App;
using SocialNetwork.App.Dtos;


namespace SocialNetwork.AspNet.Controllers
{
    
    /// <summary>
    /// Users service
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegistrationUsersService service;
        

        public AuthController(IRegistrationUsersService service)
        {
            this.service = service;
        }
        
        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="data">Data of new user.</param>
        /// <returns>Access token</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(TokenResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResultDto>> RegisterUser(RegisterUserData data)
        {
            var newUser = await service.RegisterUser(data);
            //var token = await IssueToken(newUser, data.Password);
            return Ok(TokenResultDto.FromResponse(null));
        }
    }
}