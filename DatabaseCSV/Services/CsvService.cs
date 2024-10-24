using System.Globalization;
using CsvHelper;
using DatabaseCSV.Models;

namespace DatabaseCSV.Services;

public class CsvService : ICsvService {
    public List<Employee> GetRecords(IFormFile csvFile) {
        if (csvFile.Length == 0 || Path.GetExtension(csvFile.FileName).ToLower() != ".csv") {
            return new List<Employee>();
        }

        using var reader = new StreamReader(csvFile.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<EmployeeMap>();

        return csv.GetRecords<Employee>().ToList();
    }

}