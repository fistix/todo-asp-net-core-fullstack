using Fistix.Training.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Services
{
  public class CurrentUserService : ICurrentUserService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    //public string Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    string ICurrentUserService.Email
    {
      get => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
      set => throw new NotImplementedException();
    }
  }
}
