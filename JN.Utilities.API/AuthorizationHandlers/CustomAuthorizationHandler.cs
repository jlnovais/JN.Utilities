using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace JN.Utilities.API.AuthorizationHandlers
{
    // A custom authorization requirement 
    internal class GenericAccessRequirement : IAuthorizationRequirement
    {
        public string ResourceToAccess { get; private set; }

        public GenericAccessRequirement(string resourceToAccess)
        {
            ResourceToAccess = resourceToAccess;
        }
    }

    internal class CustomAuthorizationHandler : AuthorizationHandler<GenericAccessRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizationHandler(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            
        }

        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, GenericAccessRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(requirement.ResourceToAccess))
                // Mark the requirement as satisfied
                context.Succeed(requirement);
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}