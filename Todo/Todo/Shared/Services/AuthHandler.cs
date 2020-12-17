using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Shared.Services
{
  public class AuthHandler
  {
    private readonly IAccessTokenProvider TokenProvider = null;
    public AuthHandler(IAccessTokenProvider tokenProvider)
    {
      TokenProvider = tokenProvider;
    }
    public async Task<string> GetAuthAccessToken()
    {
      var tokenResult = await TokenProvider.RequestAccessToken();

      if (tokenResult.TryGetToken(out var token))
      {
        return token.Value;
      }

      return "";
    }
  }
}
