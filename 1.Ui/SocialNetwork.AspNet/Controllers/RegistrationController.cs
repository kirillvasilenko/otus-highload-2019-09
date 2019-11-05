using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Dtos;
using SocialNetwork.Model;

namespace SocialNetwork.AspNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService service;
        private readonly ILogger<UsersController> logger;

        public RegistrationController(IRegistrationService service, ILogger<UsersController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<User> RegisterUser(RegisterUserData data)
        {
            return await service.RegisterUser(data);
        }
    }
}