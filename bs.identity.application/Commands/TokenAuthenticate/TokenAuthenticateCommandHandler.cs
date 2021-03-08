using bs.component.sharedkernal.Exceptions;
using bs.identity.domain.Abstractions;
using bs.identity.domain.Entities;
using bs.identity.domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bs.identity.application.Commands.TokenAuthenticate
{
    public class TokenAuthenticateCommandHandler : IRequestHandler<TokenAuthenticateCommand, UserLoginResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<TokenAuthenticateCommandHandler> _logger;
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public TokenAuthenticateCommandHandler(IIdentityService identityService,
            ILogger<TokenAuthenticateCommandHandler> logger,
            SignInManager<Employee> signInManager,
            UserManager<Employee> userManager)
        {
            _identityService = identityService;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UserLoginResponseDto> Handle(TokenAuthenticateCommand request, CancellationToken cancellationToken)
        {
            var employee = await _userManager.FindByEmailAsync(request.EmailAddress);
            _logger.LogInformation($"Request send by email: {request.EmailAddress}");
            
            // Checking if user with this email exist
            if (employee is null)
            {
                _logger.LogInformation($"Email: {request.EmailAddress} is not found");
                throw new AuthenticationFailedException("Invalid Credentials");
            }

            // Checking if account is locked
            if (!employee.LockoutEnabled)
            {
                _logger.LogInformation($"Email: {request.EmailAddress} is locked");
                throw new AuthenticationFailedException("Account Locked");
            }

            // validate employee's username/password             
            var result = await _signInManager.CheckPasswordSignInAsync(employee, request.Password, false);

            if (!result.Succeeded)
            {
                _logger.LogInformation($"Email: {request.EmailAddress} provided invalid login details");
                throw new AuthenticationFailedException("Invalid Credentials");
            }

            // Calling identity service
            var identityServiceResponse = await _identityService.GenerateToken(employee.Email, request.Password, cancellationToken);

            if (!identityServiceResponse.IsSuccessStatusCode)
            {
                throw new AuthenticationFailedException("Invalid Credentials");
            }

            var serviceResponse = await identityServiceResponse.Content.ReadAsStringAsync();

            return new UserLoginResponseDto
            {
                AccessToken = JObject.Parse(serviceResponse)["access_token"].Value<string>(),
                RefreshToken = JObject.Parse(serviceResponse)["refresh_token"].Value<string>(),
                ExpireIn = JObject.Parse(serviceResponse)["expires_in"].Value<int>()
            };
        }
    }
}
