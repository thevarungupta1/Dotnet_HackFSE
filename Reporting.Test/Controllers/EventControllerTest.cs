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
    public class EventControllerTest
    {
        private readonly IEnumerable<Event> _events;
        private readonly List<Event> _eventList;
        private readonly Mock<IEventProcessor> _eventProcessorMock;

        public EventControllerTest()
        {
            _eventProcessorMock = new Mock<IEventProcessor>();
            _events = new List<Event>
            {
                 new Event(),
                  new Event()
            };
            _eventList = new List<Event>
            {
                 new Event(),
                  new Event()
            };
        }

        [Fact]
        public async Task Get_WhenEventsExist_ShouldReturnAllEvents()
        {
            //Arrange
            _eventProcessorMock.Setup(p => p.GetAll(null)).ReturnsAsync(_events);
            var controller = new EventController(_eventProcessorMock.Object);

            //Act
            var response = await controller.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Event>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetWithRelatedData_WhenEventsExist_ShouldReturnAllEventsWithRelatedTableData()
        {
            //Arrange
            _eventProcessorMock.Setup(p => p.GetEventsRelatedData(null)).ReturnsAsync(_events);
            var controller = new EventController(_eventProcessorMock.Object);

            //Act
            var response = await controller.GetWithRelatedData();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Event>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetRecentEvents_WhenEventsExist_ShouldReturnRecentEventsByCount()
        {
            //Arrange
            _eventProcessorMock.Setup(p => p.GetRecentEvents(null, 10)).ReturnsAsync(_events);
            var controller = new EventController(_eventProcessorMock.Object);

            //Act
            var response = await controller.GetRecentEvents(10);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<Event>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }
        
        [Fact]
        public async Task Post_WhenSaveValidEvents_ShouldReturnSaveSuccessTrue()
        {
            //Arrange
            _eventProcessorMock.Setup(p => p.SaveEvents(_eventList)).ReturnsAsync(true);
            var controller = new EventController(_eventProcessorMock.Object);

            //Act
            var response = await controller.Post(_eventList);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<Boolean>(okResult.Value);
            Assert.True(returnValue);
        }

        [Fact]
        public async Task Post_WhenSaveEmptyEvents_ShouldReturnBadRequest()
        {
            //Arrange
            var controller = new EventController(_eventProcessorMock.Object);

            //Act
            var response = await controller.Post(null);

            //Assert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task GetAllFocusArea_WhenFocusAreaExists_ShouldReturnAllFocusAreas()
        {
            //Arrange
            var focusAreaList = new List<string> { "fa1", "fa2" };
            _eventProcessorMock.Setup(p => p.GetAllFocusArea()).ReturnsAsync(focusAreaList);
            var controller = new EventController(_eventProcessorMock.Object);

            //Act
            var response = await controller.GetAllFocusArea();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnValue = Assert.IsType<List<string>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

    }
}
