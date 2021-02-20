using bs.component.sharedkernal.Common;
using bs.identity.api.Infrastructure.Configuration;
using bs.identity.domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace bs.identity.api.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AccountController> _logger;
        private readonly IdentityServerConfiguration _identityServerConfiguration;

        public AccountController(IHttpClientFactory httpClient, IOptions<IdentityServerConfiguration> config, ILogger<AccountController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _identityServerConfiguration = config.Value;
        }

        [HttpGet]
        public IActionResult PingService()
        {
            _logger.LogInformation("testing logs information");
            _logger.LogError("testing logs error");
            return Ok(DateTime.Now);
        }


        //https://localhost:44325/.well-known/openid-configuration

        [HttpPost("Token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserLoginResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserLoginRequestDto>> Token([FromBody] UserLoginRequestDto from)
        {
            using (var http = _httpClient.CreateClient("IdentityService"))
            {
                var result = await http.PostAsync("connect/token",
                    new FormUrlEncodedContent(SetTokenRequest(from.Username, from.Password)), default);

                var identityServiceResponse = await result.Content.ReadAsStringAsync();

                return Ok(new UserLoginResponseDto
                {
                    StatusCode = HttpStatusCode.Accepted,
                    Token = JObject.Parse(identityServiceResponse)["access_token"].Value<string>(),
                    ExpireIn = JObject.Parse(identityServiceResponse)["expires_in"].Value<int>()
                });
            }
        }

        private List<KeyValuePair<string, string>> SetTokenRequest(string email, string password)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", _identityServerConfiguration.ClientId),
                new KeyValuePair<string, string>("client_secret", _identityServerConfiguration.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password),
            };
        }
    }
}
