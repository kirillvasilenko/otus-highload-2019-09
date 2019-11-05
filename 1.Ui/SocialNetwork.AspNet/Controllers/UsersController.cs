using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Dtos;
using SocialNetwork.Model;
using SocialNetwork.Repo;

namespace SocialNetwork.AspNet.Controllers
{
    
    [ApiController]
    [Authorize]
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

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await service.GetUsers();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            try
            {
                return await service.GetUser(id);
            }
            catch (ItemNotFoundException e)
            {
                return NotFound();
            }
            
        }
    }
}