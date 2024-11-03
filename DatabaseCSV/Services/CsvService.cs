using System.Globalization;
using CsvHelper;
using DatabaseCSV.Models;

namespace DatabaseCSV.Services;

public class CsvService : ICsvService {
    public List<Employee> GetRecords(IFormFile csvFile) {
        //check file format and length
        if (csvFile.Length == 0 || Path.GetExtension(csvFile.FileName).ToLower() != ".csv") {
            return new List<Employee>();
        }
        
        //create CsvReader using CsvHelper package to read the file content
        using var reader = new StreamReader(csvFile.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        //register ClassMap to map records from the file to Employee model
        csv.Context.RegisterClassMap<EmployeeMap>();

        //get the records
        return csv.GetRecords<Employee>().ToList();
    }

}