using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using testEnv.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

namespace testEnv.Models
{
  public class MuleDBContext : DbContext
  {

    public DbSet<User> Users { get; set; }

    public MuleDBContext(DbContextOptions<MuleDBContext> options) : base(options)
    {
      //   string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NodeCourse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      //   options.UseSqlServer(connectionString);
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //   //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NodeCourse;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;user id=test_login;password= pass1234";
    //   string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NodeCourse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    //   optionsBuilder.UseSqlServer(connectionString);

    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      base.OnModelCreating(modelBuilder);

      //     modelBuilder.Entity<Category>()
      //         .Property(c => c.Name)
      //         .HasMaxLength(15)
      //         .IsRequired();

      //modelBuilder.HasDefaultSchema("public");
      modelBuilder.Entity<User>().ToTable("users");
    }

  }
}