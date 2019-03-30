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
    public class EventProcessorTest
    {
        private readonly Mock<IUnitOfWork> _repository;
        private readonly IEnumerable<Event> _events;
        private readonly List<Event> _eventList;

        public EventProcessorTest()
        {
            _repository = new Mock<IUnitOfWork>();
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
        public async Task GetAll_WhenEventsFound_ShouldReturnAllEvents()
        {
            //Arrange
            _repository.Setup(p => p.Events.GetAllAsync()).ReturnsAsync(_events);
            var processor = new EventProcessor(_repository.Object);

            //Act
            var response = await processor.GetAll(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetEventsRelatedData_WhenEventsFound_ShouldReturnAllEventsWithRelatedTable()
        {
            //Arrange
            _repository.Setup(p => p.Events.GetEventsRelatedData()).ReturnsAsync(_events);
            var processor = new EventProcessor(_repository.Object);

            //Act
            var response = await processor.GetEventsRelatedData(null);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetRecentEvents_WhenEventsFound_ShouldReturnRecentEvents()
        {
            //Arrange
            _repository.Setup(p => p.Events.GetAllAsync()).ReturnsAsync(_events);
            var processor = new EventProcessor(_repository.Object);

            //Act
            var response = await processor.GetRecentEvents(null, 5);

            //Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task SaveEvents_WhenSaveEvents_ShouldReturnSaveSuccessAsTrue()
        {
            //Arrange
            _repository.Setup(p => p.Events.AddRangeAsync(_events)).ReturnsAsync(true);
            _repository.Setup(p => p.Complete()).Returns(1);
            var processor = new EventProcessor(_repository.Object);

            //Act
            var response = await processor.SaveEvents(_eventList);

            //Assert
            var returnValue = Assert.IsType<bool>(response);
            Assert.True(returnValue);
        }

        [Fact]
        public async Task GetAllFocusArea_WhenFocusAreasExist_ShouldReturnAllFocusAreas()
        {
            //Arrange
            var focusArea = new List<string> { "fa1", "fa2" };
            _repository.Setup(p => p.Events.GetAllAsync()).ReturnsAsync(_events);
            var processor = new EventProcessor(_repository.Object);

            //Act
            var response = await processor.GetAllFocusArea();

            //Assert
            Assert.NotNull(response);
        }
    }
}
