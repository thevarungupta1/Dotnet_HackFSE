using Outreach.Reporting.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outreach.Reporting.Business.Interfaces
{
    public interface IAssociateProcessor
    {
        bool SaveAssociates(List<AssociateModel> associates);
    }
}
