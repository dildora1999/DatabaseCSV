using System.Diagnostics;
using DatabaseCSV.Database;
using DatabaseCSV.Models;
using DatabaseCSV.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCSV.Controllers;

public class HomeController(IEmployeeRepository employeeRepository, ICsvService csvService)
    : Controller {

    public async Task<IActionResult> Index() {
        var employees = await employeeRepository.GetEmployeesAsync();
        return View(employees);
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? csvFile) {
        if (csvFile is null) {
            return RedirectToAction("Error");
        }
        var records = csvService.GetRecords(csvFile);
        await employeeRepository.AddEmployeesAsync(records);
        
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}