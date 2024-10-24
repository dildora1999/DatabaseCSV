using DatabaseCSV.Models;

namespace DatabaseCSV.Database;

public interface IEmployeeRepository {
    Task AddEmployeesAsync(List<Employee> employees);
    Task<List<Employee>> GetEmployeesAsync();
}