using DatabaseCSV.Models;

namespace DatabaseCSV.Services;

public interface ICsvService {
    List<Employee> GetRecords(IFormFile csvFile);
}