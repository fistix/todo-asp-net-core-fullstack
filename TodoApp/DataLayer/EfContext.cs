using Fistix.Training.Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.DataLayer
{
  public class EfContext : DbContext
  {
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      TaskModelConfig(builder);
      ProfileModelConfig(builder);

      base.OnModelCreating(builder);
    }
    private void TaskModelConfig(ModelBuilder builder)
    {
      builder.Entity<Task>().ToTable("Tasks");
      builder.Entity<Task>().HasKey(x => x.Id);
    }
    private void ProfileModelConfig(ModelBuilder builder)
    {
      builder.Entity<Profile>().ToTable("Profiles");
      builder.Entity<Profile>().HasKey(x => x.Id);
    }
  }
}
