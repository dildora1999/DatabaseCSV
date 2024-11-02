using System.Diagnostics;
using DatabaseCSV.Controllers;
using DatabaseCSV.Database;
using DatabaseCSV.Models;
using DatabaseCSV.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace DatabaseCSV.UnitTests.Controllers;

[TestFixture]
public class HomeControllerTests : IDisposable {
    private Mock<IEmployeeRepository> _mockEmployeeRepo;
    private Mock<ICsvService> _mockCsvService;
    private HomeController _controller;

    [SetUp]
    public void Setup() {
        _mockEmployeeRepo = new Mock<IEmployeeRepository>();
        _mockCsvService = new Mock<ICsvService>();
        _controller = new HomeController(_mockEmployeeRepo.Object, _mockCsvService.Object);
    }
 
    [Test]
    public async Task Index_ReturnsViewWithEmployees() {
        var mockEmployees = new List<Employee> {
            new() { PayrollNumber = "1", Forename = "Emma" },
            new() { PayrollNumber = "2", Forename = "Jane" }
        };
        _mockEmployeeRepo.Setup(repo => repo.GetEmployeesAsync()).ReturnsAsync(mockEmployees);

        var result = await _controller.Index();
        result.Should().BeOfType<ViewResult>().Which.ViewData.Model.Should().BeEquivalentTo(mockEmployees);
    }

    [Test]
    public async Task Upload_WithNullFile_ReturnsRedirectToError() {
        var result = await _controller.Upload(null);
            
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Error");
    }
        
    [Test]
    public async Task Upload_WithValidFile_ReturnsRedirectToIndex() {
        var mockFile = new Mock<IFormFile>();
        var content = "PayrollNumber,Forename\n1,John\n2,Jane";
        var fileName = "employees.csv";
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        await writer.WriteAsync(content);
        await writer.FlushAsync();
        memoryStream.Position = 0;
        mockFile.Setup(f => f.OpenReadStream()).Returns(memoryStream);
        mockFile.Setup(f => f.FileName).Returns(fileName);
        mockFile.Setup(f => f.Length).Returns(memoryStream.Length);
        var mockEmployees = new List<Employee> {
            new() { PayrollNumber = "1", Forename = "Emma" },
            new() { PayrollNumber = "2", Forename = "Jane" }
        };
        _mockCsvService.Setup(service => service.GetRecords(It.IsAny<IFormFile>())).Returns(mockEmployees);

        var result = await _controller.Upload(mockFile.Object);
            
        _mockEmployeeRepo.Verify(repo => repo.AddEmployeesAsync(mockEmployees), Times.Once);
        result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Index");
    }

    [Test]
    public void Error_ReturnsViewWithErrorModel() {
        var activity = new Activity("Test");
        activity.Start();
        Activity.Current = activity;
            
        var result = _controller.Error();
            
        result.Should().BeOfType<ViewResult>().Which.ViewData.Model.Should().BeOfType<ErrorViewModel>()
            .Which.RequestId.Should().Be(activity.Id);
            
    }
    public void Dispose() {
        _controller?.Dispose();
    }
}