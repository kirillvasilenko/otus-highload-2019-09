using System;
using System.Threading.Tasks;
using AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthService.Users;
using AuthService.Users.Dtos;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.AspNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationUsersService service;
        private readonly ILogger<RegistrationController> logger;

        public RegistrationController(IRegistrationUsersService service, ILogger<RegistrationController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AuthUser>> RegisterUser(RegisterUserData data)
        {
            try
            {
                return await service.RegisterUser(data);
            }
            catch (UserRegistrationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}