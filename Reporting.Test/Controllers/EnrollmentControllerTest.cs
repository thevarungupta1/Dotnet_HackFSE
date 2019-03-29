using Microsoft.AspNetCore.Mvc;
using Moq;
using Outreach.Reporting.Business.BusinessEntities;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;
using Outreach.Reporting.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controllers
{
    public class EnrollmentControllerTest
    {
        private readonly IEnumerable<Enrollment> _enrollments;
        private readonly Mock<IEnrollmentProcessor> _enrollmentProcessorMock;

        public EnrollmentControllerTest()
        {
            _enrollmentProcessorMock = new Mock<IEnrollmentProcessor>();
            _enrollments = new List<Enrollment>
            {
                 new Enrollment()
                 {
                     EnrollmentID = 1,
                     EventDate = new DateTime(),
                     AssociateID = 123456,
                     EventID = "EVNT00047261",
                     CreatedBy = "test"
                 },
                  new Enrollment()
                 {
                     EnrollmentID = 2,
                     EventDate = new DateTime(),
                     AssociateID = 123456,
                     EventID = "EVNT00047261",
                     CreatedBy = "test"
                 }
            };
        }

        [Fact]
        public async Task Get_WhenEnrollmentsExist_ShouldReturnAllEnrollments()
        {
            //Arrange
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("UserId", "123456");
            dict.Add("role", "Admin");
            _enrollmentProcessorMock.Setup(p => p.GetAll(null)).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetAsync();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetEnrolledAssociates_WhenEnrollmentsExist_ShouldReturnAllEnrollments()
        {
            //Arrange
            _enrollmentProcessorMock.Setup(p => p.GetEnrolledAssociates(null)).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetEnrolledAssociates();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetUniqueVolunteersByDate_WhenEnrollmentsMatchedWithGivenParameters_ShouldReturnFilteredEnrollments()
        {
            //Arrange
            _enrollmentProcessorMock.Setup(p => p.GetUniqueVolunteersByDate("01/01/2019", "01/01/2019", null)).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetUniqueVolunteersByDate("01/01/2019", "01/01/2019");

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetTopFrequentVolunteers_WhenEnrollmentsMatchedWithGivenParameters_ShouldReturnFilteredEnrollments()
        {
            //Arrange
            var _associates = new List<Associate> { new Associate(), new Associate() };
            _enrollmentProcessorMock.Setup(p => p.GetTopFrequentVolunteers(10, null)).ReturnsAsync(_associates);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetTopFrequentVolunteers(10);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Associate>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetTopFrequentVolunteers_WhenParameterIsZero_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetTopFrequentVolunteers(0);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetTopVolunteerData_WhenDataExist_ShouldReturnTopEnrollments()
        {
            //Arrange
            var dict = new Dictionary<string, List<decimal>>();
            dict.Add("key1", new List<decimal> { 1, 2 });
            _enrollmentProcessorMock.Setup(p => p.GetTopVolunteerData(null)).ReturnsAsync(dict);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetTopVolunteerData();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<Dictionary<string, List<decimal>>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetYearlyVolunteersCount_WhenValidYearsCountPassed_ShouldReturnEnrollmentsOnGivenYears()
        {
            //Arrange
            var dict = new Dictionary<int, List<int>>();
            dict.Add(1, new List<int> { 1, 2 });
            _enrollmentProcessorMock.Setup(p => p.GetYearlyVolunteersCount(5, null)).ReturnsAsync(dict);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetYearlyVolunteersCount(5);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<Dictionary<int, List<int>>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetYearlyVolunteersCount_WhenYearsCountIsZero_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetYearlyVolunteersCount(0);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetYearlyBuWiseVolunteersCount_WhenYearsIsNotZero_ShouldReturnEnrollmentsOnGivenYears()
        {
            //Arrange
            var dict = new Dictionary<string, int>();
            dict.Add("key", 15);
            var dictList = new List<Dictionary<string, int>> { dict };
            _enrollmentProcessorMock.Setup(p => p.GetYearlyBuWiseVolunteersCount(5, null)).ReturnsAsync(dictList);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetYearlyBuWiseVolunteersCount(5);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Dictionary<string, int>>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetYearlyBuWiseVolunteersCount_WhenYearsIsZero_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetYearlyBuWiseVolunteersCount(0);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetDesignationWiseVolunteersCount_WhenEnrollmentsExist_ShouldReturnDesignationWiseVolunteersCount()
        {
            //Arrange
            var dict = new Dictionary<string, int>();
            dict.Add("key", 15);
            _enrollmentProcessorMock.Setup(p => p.GetDesignationWiseVolunteersCount(null)).ReturnsAsync(dict);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetDesignationWiseVolunteersCount();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<Dictionary<string, int>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetDesignationWiseVolunteersByYear_WhenYearsIsNotZero_ShouldReturnDesignationWiseVolunteersByYear()
        {
            //Arrange
            var lst = new List<NewRepeatedVolunteersByYear> { new NewRepeatedVolunteersByYear(), new NewRepeatedVolunteersByYear() };
            _enrollmentProcessorMock.Setup(p => p.GetDesignationWiseNewRepeatedVolunteersCountByYear(5, null)).ReturnsAsync(lst);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetDesignationWiseVolunteersByYear(5);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<NewRepeatedVolunteersByYear>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetDesignationWiseVolunteersByYear_WhenYearsIsZero_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetDesignationWiseVolunteersByYear(0);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetAllNewVolunteers_WhenYearsIsNotZero_ShouldReturnDesignationWiseVolunteersByYear()
        {
            //Arrange
            _enrollmentProcessorMock.Setup(p => p.GetAllNewVolunteers(null)).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetAllNewVolunteers();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetDateWiseVolunteersCount_WhenYearsIsNotZero_ShouldReturnDesignationWiseVolunteersByYear()
        {
            //Arrange
            var dict = new Dictionary<DateTime, List<int>>();
            dict.Add(new DateTime(), new List<int> { 15, 20 });
            _enrollmentProcessorMock.Setup(p => p.GetDateWiseVolunteersCount(null)).ReturnsAsync(dict);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetDateWiseVolunteersCount();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<Dictionary<DateTime, List<int>>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task Post_WhenSaveValidEnrollments_ShouldReturnSaveSuccessTrue()
        {
            //Arrange
            _enrollmentProcessorMock.Setup(p => p.SaveEnrollments(_enrollments)).ReturnsAsync(true);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.Post(_enrollments);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<Boolean>(okResult.Value);
            Assert.True(returnValue);
        }

        [Fact]
        public async Task Post_WhenSaveEmptyEnrollments_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.Post(null);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetEnrollmentsByFilter_WhenPassValidFilters_ShouldReturnFilteredEnrollments()
        {
            //Arrange
            var filter = new ReportFilter();
            _enrollmentProcessorMock.Setup(p => p.GetEnrollmentsByFilter(null, filter)).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetEnrollmentsByFilter(filter);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetEnrollmentsByFilter_WhenFilterIsNull_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetEnrollmentsByFilter(null);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetEnrollmentsByFilterId_WhenFilterIdIsValid_ShouldReturnEnrollmentsByFilterId()
        {
            //Arrange
            _enrollmentProcessorMock.Setup(p => p.GetEnrollmentsByFilterId(1)).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetEnrollmentsByFilterId(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetEnrollmentsByFilterId_WhenFilterIdIsZero_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetEnrollmentsByFilterId(0);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetBusinessUnits_WhenBusinessUnitsExist_ShouldReturnAllBusinessUnits()
        {
            //Arrange
            var businessUnits = new List<string> { "bu1", "bu2" };
            _enrollmentProcessorMock.Setup(p => p.GetBusinessUnits()).ReturnsAsync(businessUnits);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetBusinessUnits();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<string>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetBaseLocations_WhenBaseLocationsExist_ShouldReturnAllBaseLocations()
        {
            //Arrange
            var baseLocations = new List<string> { "bl1", "bl2" };
            _enrollmentProcessorMock.Setup(p => p.GetBaseLocations()).ReturnsAsync(baseLocations);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetBaseLocations();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<string>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }
    }
}
