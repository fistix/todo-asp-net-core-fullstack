using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
  public class TodoDbContext : DbContext
  {
    public DbSet<Todo> Todos { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //  builder.Entity<Todo>().HasKey(x => x.Id);

    //  base.OnModelCreating(builder);
    //}
  }
}
