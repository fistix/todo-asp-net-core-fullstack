using Fistix.Training.Core;
using Fistix.Training.Core.Config;
using Fistix.Training.Core.Validators.Tasks;
using Fistix.Training.DataLayer.Repositories;
using Fistix.Training.Domain.Commands.Tasks;
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
using System.Linq;
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
      });

      services.AddAuthorization();


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
              AuthorizationUrl =
                        new Uri(
                            $"{MasterConfig.Auth0Config.Domain}authorize?audience={MasterConfig.Auth0Config.Audience}"),
              Scopes = new Dictionary<string, string>()
                            {
                                {"openid profile email", "Get all required info from Auth0" }
                            }
            }
          }
        });

        c.OperationFilter<SecurityRequirementsOperationFilter>();
      });


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
      app.UseAuthentication();
      app.UseAuthorization();

     

      app.UseSwagger();
      app.UseSwaggerUI(c=> c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
