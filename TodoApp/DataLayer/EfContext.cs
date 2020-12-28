﻿using Fistix.Training.Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.DataLayer
{
    public class EfContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            TaskModelConfig(builder);
            TodoModelConfig(builder);
            base.OnModelCreating(builder);
        }
        private void TaskModelConfig(ModelBuilder builder)
        {
            builder.Entity<Task>().ToTable("Tasks");
            builder.Entity<Task>().HasKey(x => x.Id);
        }
        private void TodoModelConfig(ModelBuilder builder)
        {
            builder.Entity<Todo>().ToTable("Todos");
            builder.Entity<Todo>().HasKey(x => x.Id);
        }
    }
}