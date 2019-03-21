using Moq;
using Outreach.Reporting.Business.Processors;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
        public void GetAll()
        {
            //Arrange
            _repository.Setup(r => r.Associates.GetAll()).Returns(_associates);
            var processor = new AssociateProcessor(_repository.Object);

            //Act
            var output = processor.GetAll();

            //Assert
            Assert.NotNull(output);
            _repository.VerifyAll();
            Assert.NotNull(output);
        }

        [Fact]
        public void GetAll_Null_InvalidObject()
        {
            //Arrange
            List<Associate> lstAssociates = null;
            _repository.Setup(r => r.Associates.GetAll()).Returns(lstAssociates);
            var processor = new AssociateProcessor(_repository.Object);

            //Act
            var output = processor.GetAll();

            //Assert
            Assert.Null(output);
            _repository.VerifyAll();
        }

        [Fact]
        public void GetAll_ThrowsException()
        {
            //Arrange
            _repository.Setup(r => r.Associates.GetAll()).Throws(new Exception());
            var processor = new AssociateProcessor(_repository.Object);

            //Act
            var exception = Record.Exception(() => processor.GetAll());

            //Assert
            Assert.IsType<Exception>(exception);
            Assert.Equal("Exception of type 'System.Exception' was thrown.", exception.Message);
        }
    }
}
