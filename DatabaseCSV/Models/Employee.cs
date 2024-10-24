using CsvHelper.Configuration;

namespace DatabaseCSV.Models;

public class Employee {
    public string Payroll_Number { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string Date_of_Birth { get; set; }
    public string Telephone { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string Address_2 { get; set; }
    public string PostCode { get; set; }
    public string EMail_Home { get; set; }
    public string Start_Date { get; set; }
}

public sealed class EmployeeMap : ClassMap<Employee> {
    public EmployeeMap() {
        Map(m => m.Payroll_Number).Name("Personnel_Records.Payroll_Number");
        Map(m => m.Forename).Name("Personnel_Records.Forenames");
        Map(m => m.Surname).Name("Personnel_Records.Surname");
        Map(m => m.Address).Name("Personnel_Records.Address");
        Map(m => m.Address_2).Name("Personnel_Records.Address_2");
        Map(m => m.Mobile).Name("Personnel_Records.Mobile");
        Map(m => m.Telephone).Name("Personnel_Records.Telephone");
        Map(m => m.Date_of_Birth).Name("Personnel_Records.Date_of_Birth");
        Map(m => m.PostCode).Name("Personnel_Records.Postcode");
        Map(m => m.EMail_Home).Name("Personnel_Records.EMail_Home");
        Map(m => m.Start_Date).Name("Personnel_Records.Start_Date");
    }
}