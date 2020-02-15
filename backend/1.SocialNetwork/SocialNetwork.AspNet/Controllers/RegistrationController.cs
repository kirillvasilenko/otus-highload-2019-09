using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.App;
using SocialNetwork.App.Dtos;

namespace SocialNetwork.AspNet.Controllers
{
    /// <summary>
    /// Registration api
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationSvc;

        public RegistrationController(IRegistrationService registrationSvc)
        {
            this.registrationSvc = registrationSvc;
        }
        
        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="data">Data of new user.</param>
        /// <returns>Access token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RegistrationUserResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegistrationUserResult>> RegisterUser(RegisterUserData data)
        {
            var result = await registrationSvc.RegisterUser(data);
            return Ok(result);
        }
    }
}