using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Outreach.Reporting.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Outreach_Reporting_System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Outreach.Reporting.Data.Entities;

namespace RepositoryTest.Repositories
{
    public class AssociateRepositoryTest
    {
        private readonly AssociateRepository _associateRepository;
        private readonly Mock<ReportDBContext> _dbContextMock;
        public AssociateRepositoryTest()
        {
            _dbContextMock = new Mock<ReportDBContext>();
            //var client = new TestClientProvider().Client;
            //var response = await client.GetAsync("api/associate");
            //response.EnsureSuccessStatusCode();
          
            //_associateRepository = new AssociateRepository(_dbContextMock.Object);
        }

        [Fact]
        public void GetAll()
        {
            //var results = _associateRepository.GetAll();
            //Assert.Empty(results);
        }
    }
}
