using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IAssociateProcessor
    {
        bool SaveAssociates(IEnumerable<Associates> associates);
    }
}
