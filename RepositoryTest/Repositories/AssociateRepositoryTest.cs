using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Outreach.Reporting.Data.Repository;
using Outreach_Reporting_System.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace RepositoryTest.Repositories
{
    public class AssociateRepositoryTest
    {
        private readonly AssociateRepository _associateRepository;

        private static readonly IConfigurationRoot Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

        public AssociateRepositoryTest()
        {
            var appSettingAccessorMock = new Mock<IOptions<AppSettings>>();
            var appSettingAccessor = new AppSettings
            {
                DatabaseConnectionString = Config["connectionstring"]
            };
            appSettingAccessorMock.Setup(ap => ap.Value).Returns(appSettingAccessor);
            //_associateRepository = new AssociateRepository(appSettingAccessorMock.Object);
        }
    }
}
