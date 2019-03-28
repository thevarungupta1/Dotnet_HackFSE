using Moq;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using Outreach.Reporting.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthProcessor> _authProcessor;


        public AuthControllerTest()
        {
            _authProcessor = new Mock<IAuthProcessor>();
        }

        [Fact]
        public void Authenticate_WhenAuthenticateSuccess_ShouldReturnValidToken()
        {
            //Arrange
            int associateId = 123456;
            string email = "test@test.com";
            var processor = new Mock<IAuthProcessor>();
            processor.Setup(p => p.AuthenticateUser(associateId, email)).Returns(true);
            var controller = new AuthController(processor.Object);

            //Act
            //var response = controller.Authenticate(associateId, email);

            //Assert
           // Assert.NotNull(response);
        }
    }
}
