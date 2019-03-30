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
using System.Threading.Tasks;
using Xunit;

namespace Reporting.Test.Controllers
{
    public class AssociateControllerTest
    {
        private readonly IEnumerable<Associate> _associates;
        private readonly List<Associate> _associateList;
        private readonly Mock<IAssociateProcessor> _associateProcessorMock;

        public AssociateControllerTest()
        {
            _associateProcessorMock = new Mock<IAssociateProcessor>();
            _associates = new List<Associate>
            {
                 new Associate(),
                 new Associate()
            };
            _associateList = new List<Associate>
            {
                 new Associate(),
                 new Associate()
            };               
        }
        [Fact]
        public async Task Get_WhenAssociatesFound_ShouldReturnAllAssociates()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            _associateProcessorMock.Setup(p => p.GetAll()).ReturnsAsync(_associates);
            var controller = new AssociateController(_associateProcessorMock.Object);

            //Act
            var response = await controller.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Associate>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task Get_WhenNoAssociatesFound_ShourReturnEmptyList()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            //_unitOfWorkMock.Setup(repo => repo.Associates.GetAll()).Throws(null);
            _associateProcessorMock.Setup(p => p.GetAll()).ReturnsAsync(new List<Associate>());
            var controller = new AssociateController(_associateProcessorMock.Object);

            //Act
            var response = await controller.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Associate>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task Post_WhenSaveAssociatesSuccess_ShourReturnTrue()
        {
            //Arrange
            _associateProcessorMock.Setup(p => p.SaveAssociates(_associateList)).ReturnsAsync(true);
            var controller = new AssociateController(_associateProcessorMock.Object);

            //Act
            var response = await controller.Post(_associateList);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            //var idea = returnValue.FirstOrDefault();
            Assert.True(returnValue);
        }

        [Fact]
        public async Task Post_WhenSaveAssociatesFailed_ShourReturnFalse()
        {
            //Arrange
            _associateProcessorMock.Setup(p => p.SaveAssociates(_associateList)).ReturnsAsync(false);
            var controller = new AssociateController(_associateProcessorMock.Object);

            //Act
            var response = await controller.Post(_associateList);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            //var idea = returnValue.FirstOrDefault();
            Assert.False(returnValue);
        }

        [Fact]
        public async Task Post_WhenAssociatesListIsEmpty_ShourReturnBadRequest()
        {
            //Arrange
            var controller = new AssociateController(_associateProcessorMock.Object);

            //Act
            var response = await controller.Post(null);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

    }
}
