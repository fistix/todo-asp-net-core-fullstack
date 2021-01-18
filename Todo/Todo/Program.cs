using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Todo.Shared;
using Todo.Shared.Services;
using Todo.Shared.State;

namespace Todo
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
      builder.Services.AddScoped<AppState>();
      builder.Services.AddScoped<AuthHandler>();
      builder.Services.AddScoped<RequestHandler>();
      builder.Services.AddSyncfusionBlazor();

      builder.Services.AddOidcAuthentication(options =>
      {
        builder.Configuration.Bind("Auth0", options.ProviderOptions);
        options.ProviderOptions.ResponseType = "code";
      });

      //builder.Services.AddBlazorAuth0(options =>
      //{
      //  // Required
      //  options.Domain = "https://dev-l0eer1xl.us.auth0.com";

      //  // Required
      //  options.ClientId = "3xh1AQ1uS55ie56YmupAb92Mp5UYH4gD";

      //  //// Required if you want to make use of Auth0's RBAC
      //  options.Audience = "https://localhost:44372/";

      //  //// Uncomment the following line if you don't want your users to be automatically logged-off on token expiration
      //  // options.SlidingExpiration = true;

      //  //// Uncomment the following two lines if you want your users to log in via a pop-up window instead of being redirected
      //  // options.LoginMode = LoginModes.Popup;

      //  //// Uncomment the following line if you don't want your unauthenticated users to be automatically redirected to Auth0's Universal Login page 
      //  // options.RequireAuthenticatedUser = false;
      //});

      builder.Services.AddAuthorizationCore();


      await builder.Build().RunAsync();
    }
  }
}
