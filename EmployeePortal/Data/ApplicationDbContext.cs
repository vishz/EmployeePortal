using EmployeePortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeePortal.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
  }
}
