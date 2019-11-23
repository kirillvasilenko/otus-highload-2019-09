using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthService.Users;
using AuthService.Users.Dtos;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;


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

        private readonly ITokenResponseGenerator responseGenerator;
        private readonly ITokenRequestValidator requestValidator;

        public RegistrationController(IRegistrationUsersService service, ITokenResponseGenerator responseGenerator, ITokenRequestValidator requestValidator)
        {
            this.service = service;
            this.responseGenerator = responseGenerator;
            this.requestValidator = requestValidator;
        }
        
        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="data">Data of new user.</param>
        /// <returns>Access token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TokenResponse>> RegisterUser(RegisterUserData data)
        {
            var newUser = await service.RegisterUser(data);
            var token = IssueToken(newUser, data.Password);
            return Ok(token);
        }
        
        private async Task<TokenResponse> IssueToken(AuthUser newUser, string password)
        {
            var clientResult = new ClientSecretValidationResult
            {
                Client = Config.WebAppClient
            };
            var names = new NameValueCollection
            {
                {OidcConstants.TokenRequest.GrantType, GrantType.ResourceOwnerPassword},
                {OidcConstants.TokenRequest.UserName, newUser.Email},
                {OidcConstants.TokenRequest.Password, password},
                {OidcConstants.TokenRequest.Scope, Config.WebAppScopes},
                {OidcConstants.TokenRequest.ClientId, Config.WebAppClient.ClientId}
            };
            
            var requestResult = await requestValidator.ValidateRequestAsync(names, clientResult);
            
            var response = await responseGenerator.ProcessAsync(requestResult);

            return response;
        }
    }
}