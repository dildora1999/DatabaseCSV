using DatabaseCSV.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace DatabaseCSV.UnitTests.Services;

[TestFixture]
public class CsvServiceTests {
    private CsvService _csvService;

    [SetUp]
    public void Setup() {
        _csvService = new CsvService();
    }

    [Test]
    public void GetRecords_EmptyFile_ReturnsEmptyList() {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(0);
        fileMock.Setup(f => f.FileName).Returns("employees.csv");
        
        var result = _csvService.GetRecords(fileMock.Object);

        result.Should().BeEmpty();
    }

    [Test]
    public void GetRecords_InvalidFileExtension_ReturnsEmptyList() {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(100);
        fileMock.Setup(f => f.FileName).Returns("employees.txt"); // Invalid extension
        
        var result = _csvService.GetRecords(fileMock.Object);
        
        result.Should().BeEmpty();
    }

    [Test]
    public void GetRecords_ValidCsvFile_ReturnsEmployeeList() {
        var csvContent = "Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address,Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date" +
                         "\nCOOP08,John,William,26/01/1955,12345678,987654231,12 Foreman road,London,GU12 6JW,nomadic20@hotmail.co.uk,18/04/2013";
        var fileMock = new Mock<IFormFile>();
        var contentStream = new MemoryStream();
        var writer = new StreamWriter(contentStream);
        writer.Write(csvContent);
        writer.Flush();
        contentStream.Position = 0;
        fileMock.Setup(f => f.Length).Returns(contentStream.Length);
        fileMock.Setup(f => f.FileName).Returns("employees.csv");
        fileMock.Setup(f => f.OpenReadStream()).Returns(contentStream);
        
        var result = _csvService.GetRecords(fileMock.Object);

        result.Should().NotBeNull();
        result.Count.Should().Be(1);
        result[0].PayrollNumber.Should().Be("COOP08");
        result[0].Forename.Should().Be("John");
        result[0].Surname.Should().Be("William");
        result[0].DateOfBirth.Should().Be("26/01/1955");
        result[0].Telephone.Should().Be("12345678");
        result[0].Mobile.Should().Be("987654231");
        result[0].Address.Should().Be("12 Foreman road");
        result[0].Address2.Should().Be("London");
        result[0].PostCode.Should().Be("GU12 6JW");
        result[0].EmailHome.Should().Be("nomadic20@hotmail.co.uk");
        result[0].StartDate.Should().Be("18/04/2013");
    }
}
