using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lambor.Services.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lambor.Tools
{
    public class ApiAuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        private const string ApiKeyHeaderKey = "apikey";

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(ApiKeyHeaderKey))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                var apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderKey];
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    var userManager = (IApplicationUserManager)context.HttpContext.RequestServices.GetService(typeof(IApplicationUserManager));
                    var user = await userManager?.FindByApiKey(apiKey);
                    if (user == null)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    else
                    {
                        context.HttpContext.User = new ClaimsPrincipal(new List<ClaimsIdentity>
                        {
                            new(new[] {new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())})
                        });
                    }
                }

            }

        }
    }
}
