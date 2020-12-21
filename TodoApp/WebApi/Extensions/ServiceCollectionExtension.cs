using Core;
using DataLayer.Repositories;
using Domain.Commands.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Service.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Extensions
{
  public static class ServiceCollectionExtension
  {
    public static void AddCommonServices(this IServiceCollection services)
    {
      services.AddMediatR(typeof(CreateTaskCommand).Assembly,typeof(CreateTaskCommandHandler).Assembly);
      services.AddScoped<ITodoRepository, TodoRepository>();
    }
  }
}
