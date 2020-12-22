using Fistix.Training.DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Fistix.Training.DataLayer.Repositories;
using Fistix.Training.Core;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using Fistix.Training.Service;
using Fistix.Training.Domain.Commands;
using Fistix.Training.Service.CommandHandlers;

namespace TodoApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAutoMapper(typeof(MapperProfile));
      services.AddMediatR(typeof(AddTodoCommand), typeof(AddTodoCommandHandler));// typeof(AddTodoCommand).GetTypeInfo().Assembly);

      services.AddControllers();

      services.AddCors(options =>
      {
        // this defines a CORS policy called "default"
        options.AddPolicy("default", policy =>
        {
          policy.WithOrigins("https://localhost:44377", "http://localhost:60623")
              .AllowAnyHeader()
              .AllowAnyMethod();
        });
      });

      string swaggerDescription = "";
      if (!string.IsNullOrEmpty(Configuration["Auth0:ClientId"]))
      {
        swaggerDescription = $"Use ClientId: <b>{Configuration["Auth0:ClientId"]}</b> to authorize<br /><u style='color: red;'>Be sure <b>openid profile email</b> has to be checked on authrization popup to get user Info from Auth0</u>";
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
                            $"{Configuration["Auth0:Domain"]}authorize?audience={Configuration["Auth0:Audience"]}"),
              Scopes = new Dictionary<string, string>()
                            {
                                {"openid profile email", "Get all required info from Auth0" }
                            }
            }
          }
        });

        c.OperationFilter<SecurityRequirementsOperationFilter>();
      });




      services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("TodoDatabase")));

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.Authority = Configuration["Auth0:Domain"];
        options.Audience = Configuration["Auth0:Audience"];
      });

      services.AddAuthorization();

      services.AddScoped<ITodoRepository, TodoRepository>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp v1"));
      }
      app.UseCors("default");

      app.UseHttpsRedirection();

      app.UseRouting();

      
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
