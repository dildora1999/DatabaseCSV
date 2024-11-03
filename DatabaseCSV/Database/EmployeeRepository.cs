using DatabaseCSV.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseCSV.Database;

public class EmployeeRepository (EmployeeDbContext employeeDbContext) : IEmployeeRepository {
    public async Task AddEmployeesAsync(List<Employee> employees) {
        if (!employees.Any())
            return;
        //code first approach that creates database named Employees in case it was not found
        await employeeDbContext.Database.EnsureCreatedAsync();
        
        //get only new Employees from the list
        var newEmployees = employees
            .Where(e => employeeDbContext.Employees.Find(e.PayrollNumber) == null)
            .ToList();
        
        //insert them into database
        if (newEmployees.Any()) {
            await employeeDbContext.Employees.AddRangeAsync(newEmployees);
            await employeeDbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Employee>> GetEmployeesAsync() {
        //code first approach that creates database named Employees in case it was not found
        await employeeDbContext.Database.EnsureCreatedAsync();
        
        //get all Employees from the database
        return await employeeDbContext.Employees.ToListAsync();
    }

}