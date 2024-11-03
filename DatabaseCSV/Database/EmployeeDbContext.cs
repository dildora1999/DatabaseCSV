using DatabaseCSV.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCSV.Database;

public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext(options) {
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        //create Employees table with PayrollNumber as a primary key
        modelBuilder.Entity<Employee>(entity => {
            entity.HasKey(e => e.PayrollNumber);
        });
    }
}