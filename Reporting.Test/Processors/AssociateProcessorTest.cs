using Moq;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reporting.Test.Processors
{
    public class AssociateProcessorTest
    {
        private readonly Mock<IUnitOfWork> _repository;
        private readonly IEnumerable<Associate> _associates;

        public AssociateProcessorTest()
        {
            _repository = new Mock<IUnitOfWork>();
            _associates = new List<Associate>
            {
                 new Associate(),
                 new Associate()
            };
        }

        [Fact]
        public async Task GetAll_WhenAssociatesFound_ShouldReturnAllAssociates()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            _repository.Setup(p => p.Associates.GetAllAsync()).ReturnsAsync(_associates);
            var processor = new AssociateProcessor(_repository.Object);

            //Act
            var response = await processor.GetAll();

            //Assert
            var returnValue = Assert.IsType<List<Associate>>(response);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task SaveAssociates_WhenSaveAssociates_ShouldReturnTrue()
        {
            //Arrange
            //var mockId = It.IsAny<int?>();
            _repository.Setup(p => p.Associates.AddRangeAsync(_associates)).ReturnsAsync(true);
            _repository.Setup(p => p.Complete()).Returns(1);
            var processor = new AssociateProcessor(_repository.Object);

            //Act
            var response = await processor.SaveAssociates(_associates);

            //Assert
            var returnValue = Assert.IsType<bool>(response);
            Assert.True(returnValue);
        }
    }
}
