using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationSamples
{
    public class OfficeHoursRequirement : IAuthorizationRequirement
    {
        public OfficeHoursRequirement(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; private set; }
        public int End { get; private set; }
    }

    public class OfficeHoursRequirementHandler : AuthorizationHandler<OfficeHoursRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OfficeHoursRequirement requirement)
        {
            var now = DateTime.Now;

            if (now.Hour >= requirement.Start && now.Hour <= requirement.End)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
