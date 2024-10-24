using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;

namespace DatabaseCSV.Models;

public class Employee {
    [MaxLength(50)]
    public string PayrollNumber { get; set; }
    [MaxLength(100)]
    public string Forename { get; set; }
    [MaxLength(100)]
    public string Surname { get; set; }
    [MaxLength(50)]
    public string DateOfBirth { get; set; }
    [MaxLength(50)]
    public string Telephone { get; set; }
    [MaxLength(50)]
    public string Mobile { get; set; }
    [MaxLength(100)]
    public string Address { get; set; }
    [MaxLength(100)]
    public string Address2 { get; set; }
    [MaxLength(50)]
    public string PostCode { get; set; }
    [MaxLength(50)]
    public string EmailHome { get; set; }
    [MaxLength(50)]
    public string StartDate { get; set; }
}

public sealed class EmployeeMap : ClassMap<Employee> {
    public EmployeeMap() {
        Map(m => m.PayrollNumber).Name("Personnel_Records.Payroll_Number");
        Map(m => m.Forename).Name("Personnel_Records.Forenames");
        Map(m => m.Surname).Name("Personnel_Records.Surname");
        Map(m => m.Address).Name("Personnel_Records.Address");
        Map(m => m.Address2).Name("Personnel_Records.Address_2");
        Map(m => m.Mobile).Name("Personnel_Records.Mobile");
        Map(m => m.Telephone).Name("Personnel_Records.Telephone");
        Map(m => m.DateOfBirth).Name("Personnel_Records.Date_of_Birth");
        Map(m => m.PostCode).Name("Personnel_Records.Postcode");
        Map(m => m.EmailHome).Name("Personnel_Records.EMail_Home");
        Map(m => m.StartDate).Name("Personnel_Records.Start_Date");
    }
}