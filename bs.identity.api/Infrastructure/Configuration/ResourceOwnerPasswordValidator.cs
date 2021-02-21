using bs.identity.domain.Entities;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bs.identity.api.Infrastructure.Configuration
{
    public class ResourceOwnerPasswordValidator : Controller, IResourceOwnerPasswordValidator
    {
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public ResourceOwnerPasswordValidator(SignInManager<Employee> signInManager, UserManager<Employee> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var employee = await _userManager.FindByEmailAsync(context.UserName);

            if (!employee.Equals(null))
            {
                var result =
                    await _signInManager.CheckPasswordSignInAsync(employee, context.Password, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (result.Succeeded) context.Result = new GrantValidationResult(
                        subject: employee.Id,
                        authenticationMethod: "custom",
                        claims: await _userManager.GetClaimsAsync(employee));
                    return;
                }
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credential");
        }
    }
}
