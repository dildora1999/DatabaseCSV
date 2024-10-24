using DatabaseCSV.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCSV.Database;

public class EmployeeRepository (EmployeeDbContext employeeDbContext) : IEmployeeRepository {
    public async Task AddEmployeesAsync(List<Employee> employees) {
        if (!employees.Any())
            return;

        await employeeDbContext.Database.EnsureCreatedAsync();

        var newEmployees = employees
            .Where(e => employeeDbContext.Employees.Find(e.Payroll_Number) == null)
            .ToList();

        if (newEmployees.Any()) {
            await employeeDbContext.Employees.AddRangeAsync(newEmployees);
            await employeeDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Employee>> GetEmployeesAsync() {
        await employeeDbContext.Database.EnsureCreatedAsync();
        return await employeeDbContext.Employees.ToListAsync();
    }

}