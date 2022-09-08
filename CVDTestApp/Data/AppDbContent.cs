using CVDTestApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CVDTestApp.Data
{
    /// <summary>
    /// For work with DB
    /// </summary>
    public class AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}