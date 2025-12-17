using Microsoft.EntityFrameworkCore;

namespace EFCoreIntroConsoleApp.Contexts;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-4FBIQCG\\SQLEXPRESS;Database=EmployeeDepartmentDb;Trusted_connection=true;TrustServerCertificate=true");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}
