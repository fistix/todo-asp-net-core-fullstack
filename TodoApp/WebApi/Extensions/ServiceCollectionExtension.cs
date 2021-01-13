using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.DataLayer;
using Fistix.Training.DataLayer.Repositories;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Queries.Tasks;
using Fistix.Training.Service;
using Fistix.Training.Service.QueryHandlers.Tasks;
using Fistix.Training.Service.CommandHandlers.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fistix.Training.Service.AzureFileService;
using Azure.Storage.Blobs;
using Fistix.Training.Core.Config;

namespace Fistix.Training.WebApi.Extensions
{
  public static class ServiceCollectionExtension
  {

    public static void AddCommonServices(this IServiceCollection services, IConfiguration Configuration, MasterConfig masterConfig)
    {
      services.AddScoped(x => masterConfig);
      services.AddAutoMapper(typeof(MapperProfile));
      services.AddMediatR(typeof(CreateTaskCommand).Assembly, typeof(CreateTaskCommandHandler).Assembly);
      services.AddScoped<ITaskRepository, TaskRepository>();
      services.AddScoped<IProfileRepository, ProfileRepository>();

      services.AddDbContext<EfContext>(options =>
        options.UseSqlServer(masterConfig.ConnectionStringConfig.TodoDatabase)
         );

      //services.AddScoped(x => new BlobServiceClient(Configuration["AzureStorageConnectionString"]));
      services.AddScoped(x => new BlobServiceClient(masterConfig.AzureStorageConfig.AzureStorageConnectionString));

      services.AddScoped<IFileService, FileService>();

    }
  }
}
