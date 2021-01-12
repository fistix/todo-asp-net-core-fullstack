using Fistix.Training.Core;
using Fistix.Training.Core.Config;
using Fistix.Training.Core.Validators.Tasks;
using Fistix.Training.DataLayer.Repositories;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.WebApi.Extensions;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
      
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers().AddFluentValidation(x=> x.RegisterValidatorsFromAssembly(typeof(CreateTaskCommandValidator).Assembly));
            services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
      });

      services.AddCommonServices(Configuration, MasterConfig);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
