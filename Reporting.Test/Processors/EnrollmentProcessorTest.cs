using Moq;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Processors
{
    public class EnrollmentProcessorTest
    {
        private readonly Mock<IUnitOfWork> _repository;
        private readonly IEnumerable<Enrollment> _enrollments;

        public EnrollmentProcessorTest()
        {
            _repository = new Mock<IUnitOfWork>();
            _enrollments = new List<Enrollment>
            {
                 new Enrollment(),
                 new Enrollment()
            };
        }

        [Fact]
        public async Task GetAll_WhenEnrollmentsFound_ShouldReturnAllEnrollments()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetAllAsync()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetAll(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetEnrolledAssociates_WhenEnrollmentsFound_ShouldReturnAllEnrolledAssociates()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetEnrolledAssociates()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetEnrolledAssociates(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetEnrolledUniqueAssociates_WhenEnrollmentsFound_ShouldReturnUniqueVolunteers()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetEnrolledAssociates()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetEnrolledUniqueAssociates(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetUniqueVolunteersByDate_WhenEnrollmentsFound_ShouldReturnUniqueVolunteersByDate()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetEnrolledAssociates()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetUniqueVolunteersByDate(null, null, null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task SaveEnrollments_WhenSaveEnrollments_ShouldReturnSaveSuccessAsTrue()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.AddRangeAsync(_enrollments)).ReturnsAsync(true);
            _repository.Setup(p => p.Complete()).Returns(1);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.SaveEnrollments(_enrollments);

            //Assert
            var returnValue = Assert.IsType<bool>(response);
            Assert.True(returnValue);
        }
                
        [Fact]
        public async Task GetAllNewVolunteers_WhenEnrollmentsFound_ShouldReturnAllNewVolunteers()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetEnrolledAssociates()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetAllNewVolunteers(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetDateWiseVolunteersCount_WhenEnrollmentsFound_ShouldReturnVolunteersByDate()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetEnrolledAssociates()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetDateWiseVolunteersCount(null);

            //Assert
            Assert.NotEmpty(response);
        }
        
        [Fact]
        public async Task GetMonthWiseVolunteersCount_WhenEnrollmentsFound_ShouldReturnTopData()
        {
            //Arrange
            _repository.Setup(p => p.Enrollments.GetEnrolledAssociates()).ReturnsAsync(_enrollments);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetMonthWiseVolunteersCount(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetBusinessUnits_WhenBusinessUnitsExist_ShouldReturnAllBusinessUnits()
        {
            //Arrange
            var businessUnits = new List<string> { "bu1", "bu2" };
            _repository.Setup(p => p.Enrollments.GetBusinessUnits()).ReturnsAsync(businessUnits);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetBusinessUnits();

            //Assert
            var returnValue = Assert.IsType<List<string>>(response);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetBaseLocations_WhenBaseLocationsExist_ShouldReturnAllBaseLocations()
        {
            //Arrange
            var baseLocations = new List<string> { "bl1", "bl2" };
            _repository.Setup(p => p.Enrollments.GetBaseLocations()).ReturnsAsync(baseLocations);
            var processor = new EnrollmentProcessor(_repository.Object);

            //Act
            var response = await processor.GetBaseLocations();

            //Assert
            var returnValue = Assert.IsType<List<string>>(response);
            Assert.NotEmpty(returnValue);
        }

    }
}
