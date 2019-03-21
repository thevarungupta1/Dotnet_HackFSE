using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using Outreach.Reporting.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Reporting.Test.Controllers
{
    public class AssociateControllerTest
    {
        //private readonly Mock<ILogger<AssociateController>> _loggerMock;
        //private readonly ILogger<AssociateController> _logger;
        private readonly IEnumerable<Associate> _associates;
        private readonly Mock<IAssociateProcessor> _associateProcessorMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Associate _associate;

        public AssociateControllerTest()
        {
            //_loggerMock = new Mock<ILogger<AssociateController>>();
            //_logger = _logger.Object;
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _associates = new List<Associate>
            {
                 new Associate(),
                 new Associate()
            };               
        }
        [Fact]
        public void TestGetAssociates_IsNotNull()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            _unitOfWorkMock.Setup(repo => repo.Associates.GetAll()).Returns(_associates);
            var processor = new AssociateProcessor(_unitOfWorkMock.Object);
            var controller = new AssociateController(processor);

            //Act
            var response = controller.Get();

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void TestGetAssociates_IsNull()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            _unitOfWorkMock.Setup(repo => repo.Associates.GetAll()).Throws(null);
            var processor = new AssociateProcessor(_unitOfWorkMock.Object);
            var controller = new AssociateController(processor);

            //Act
            var response = new ContentResult { StatusCode = (int)HttpStatusCode.NoContent };

            //Assert
            var contentResult = Assert.IsType<ContentResult>(response);
            Assert.Null(contentResult.Content);
        }

        [Fact]
        public void TestGetAssociates_BadRequest()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            _unitOfWorkMock.Setup(repo => repo.Associates.GetAll()).Throws(new NullReferenceException());
            var processor = new AssociateProcessor(_unitOfWorkMock.Object);
            var controller = new AssociateController(processor);

            //Act
            var response = controller.Get();

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
