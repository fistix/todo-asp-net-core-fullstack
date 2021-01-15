using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.AuthorizationRequirements
{
  public class CustomRequireClaim : IAuthorizationRequirement
  {
    public CustomRequireClaim(string claimType)
    {
      ClaimType = claimType;
    }
    public string ClaimType { get; set; }
  }

  public class CustomRequireClaimhandler : AuthorizationHandler<CustomRequireClaim>
  {
    IHttpContextAccessor _httpContextAccessor = null;
    public CustomRequireClaimhandler(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaim requirement)
    {
      HttpContext httpContext = _httpContextAccessor.HttpContext;
      //bool authHeader = httpContext.Request.Headers.ContainsKey("IsAdmin");
      var hasClaim = context.User.Claims.Any(x => x.Type.Equals(requirement.ClaimType));
      if (hasClaim)
      {
        
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }

  public static class AuthorizationPolicyBuilderExtensions
  {
    public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder,
      string claimType)
    {
      builder.AddRequirements(new CustomRequireClaim(claimType));
      return builder;
    }
  }
}
