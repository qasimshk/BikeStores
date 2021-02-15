using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bs.identity.api.Infrastructure.Configuration
{
    public class ResourceOwnerPasswordValidator : Controller, IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            await Task.Delay(10);

            if (context.UserName.Contains("test@test.com") && context.Password.Contains("test"))
            {
                context.Result = new GrantValidationResult(
                    "1",
                    "custom",
                    new List<Claim>
                            {
                                new(ClaimTypes.Name, "Muhammad Qasim"),
                                new(ClaimTypes.Email, "test@test.com"),
                                new(ClaimTypes.Role, "Staff"),
                                new(ClaimTypes.Gender, "male"),
                            });
                return;
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credential");
        }
    }
}
