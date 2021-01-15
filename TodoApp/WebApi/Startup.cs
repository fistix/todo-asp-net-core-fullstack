using Fistix.Training.Core;
using Fistix.Training.Core.AuthorizationRequirements;
using Fistix.Training.Core.Config;
using Fistix.Training.Core.Validators.Tasks;
using Fistix.Training.WebApi.Extensions;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi
{
  public class Startup
  {
    private MasterConfig MasterConfig = new MasterConfig();
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      MasterConfig.ConnectionStringConfig = Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsConfig>();

      MasterConfig.AzureStorageConfig = Configuration.GetSection("AzureStorage").Get<AzureStorageConfig>();
      MasterConfig.Auth0Config = Configuration.GetSection("Auth0").Get<Auth0Config>();

    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(CreateTaskCommandValidator).Assembly));

      //services.AddSwaggerGen(c =>
      //  {
      //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
      //  });

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = MasterConfig.Auth0Config.Domain;
        options.Audience = MasterConfig.Auth0Config.Audience;
        options.RequireHttpsMetadata = false;
        //
        options.Events = new JwtBearerEvents
        {
          OnTokenValidated = context =>
          {
            if (!(context.SecurityToken is JwtSecurityToken token)) return Task.CompletedTask;
            if (context.Principal.Identity is ClaimsIdentity identity)
            {
              identity.AddClaim(new Claim("access_token", token.RawData));
            }

            return Task.CompletedTask;
          }
        };
      });


      services.AddAuthorization(config =>
      {
        //config.AddPolicy("Claim.Role", policyBuilder =>
        //{
        //  policyBuilder.RequireCustomClaim(ClaimTypes.Role);
        //});

        config.AddPolicy("IsAdmin", policybuilder =>
         {
           //policybuilder.RequireCustomClaim("IsAdmin");
           policybuilder.RequireCustomClaim(ClaimTypes.Email);
         });
      });


      string swaggerDescription = "";
      if (!string.IsNullOrEmpty(MasterConfig.Auth0Config.ClientId))
      {
        swaggerDescription = $"Use ClientId: <b>{MasterConfig.Auth0Config.ClientId}</b> to authorize<br /><u style='color: red;'>Be sure <b>openid profile email</b> has to be checked on authrization popup to get user Info from Auth0</u>";
      }

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "v1", Version = "v1", Description = swaggerDescription });

        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
          Type = SecuritySchemeType.OAuth2,
          Flows = new OpenApiOAuthFlows
          {
            Implicit = new OpenApiOAuthFlow
            {
              AuthorizationUrl = new Uri($"{MasterConfig.Auth0Config.Domain}authorize?audience={MasterConfig.Auth0Config.Audience}"),
              Scopes = new Dictionary<string, string>()
              {
                {"openid profile email", "Get all required info from Auth0" }
              }
            }
          }
        });

        c.OperationFilter<SecurityRequirementsOperationFilter>();
      });

      services.AddHttpContextAccessor();
      services.AddCommonServices(/*Configuration,*/ MasterConfig);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        //app.UseSwagger();
        //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
      }

      //app.UseHttpsRedirection();

      app.UseRouting();

      //who are you?
      app.UseAuthentication();

      // are you allowed?
      app.UseAuthorization();



      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
