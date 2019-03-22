using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IFileProcessor
    {
        IEnumerable<File> GetAll();
        bool SaveFiles(IEnumerable<File> files);
        bool ReadExcelFile(string filePath);
    }
}
