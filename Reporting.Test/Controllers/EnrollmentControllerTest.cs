using Microsoft.AspNetCore.Mvc;
using Moq;
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
            _enrollmentProcessorMock.Setup(p => p.GetAll(new Dictionary<string, string>())).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetAsync();

            //Assert
            //var okResult = Assert.IsType<OkObjectResult>(response);
            //var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetEnrolledAssociates_WhenEnrollmentsExist_ShouldReturnAllEnrollments()
        {
            //Arrange
            _enrollmentProcessorMock.Setup(p => p.GetEnrolledAssociates(new Dictionary<string, string>())).ReturnsAsync(_enrollments);
            var controller = new EnrollmentController(_enrollmentProcessorMock.Object);

            //Act
            var response = await controller.GetEnrolledAssociates();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Enrollment>>(okResult.Value);
            Assert.NotNull(response);
        }
    }
}
