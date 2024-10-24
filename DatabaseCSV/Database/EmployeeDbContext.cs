using DatabaseCSV.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCSV.Database;

public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext {
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Employees;User=SA;Password=Dildorita22*;TrustServerCertificate=True");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity => {
            entity.HasKey(e => e.PayrollNumber);
        });
    }
}