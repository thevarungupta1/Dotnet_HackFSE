using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IFileProcessor
    {
        IEnumerable<File> GetAll();
        bool SaveFiles(IEnumerable<File> files);
        bool ReadExcelFile(string filePath);
    }
}
