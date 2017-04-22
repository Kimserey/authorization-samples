using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationSamples
{
    public class AuthorRequirement : IAuthorizationRequirement { }

    public class AuthorRequirementHandler : AuthorizationHandler<AuthorRequirement, Report>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AuthorRequirement requirement,
            Report resource)
        {
            if (context.User.Identity.Name == resource.Author)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
