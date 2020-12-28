﻿using AutoMapper;
using Fistix.Training.Core;
using Fistix.Training.DataLayer;
using Fistix.Training.DataLayer.Repositories;
using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Queries.Tasks;
using Fistix.Training.Service;
using Fistix.Training.Service.QueryHandlers.Tasks;
using Fistix.Training.Service.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddCommonServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddMediatR(typeof(CreateTaskCommand).Assembly, typeof(CreateTaskCommandHandler).Assembly);
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            
            services.AddDbContext<EfContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("TodoDatabase")));


        }
    }
}
