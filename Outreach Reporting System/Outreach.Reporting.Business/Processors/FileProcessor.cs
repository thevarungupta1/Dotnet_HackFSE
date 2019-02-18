using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Model;

namespace Outreach.Reporting.Business.Processors
{
    public class FileProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileProcessor(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public static bool DataTableProcess(DataTable dtContent)
        {
            if (dtContent.Rows != null && dtContent.Rows.Count > 0)
            {
                var dr = dtContent.Rows[0];
                DataColumnCollection columns = dtContent.Columns;
                if (columns.Contains("Associate ID") && columns.Contains("Name") && columns.Contains("Designation"))
                {
                    AssociatesDataProcess(dtContent);
                }
                else if (columns.Contains("Event ID") && columns.Contains("Venue Address") && columns.Contains("Event Name")  
                    && columns.Contains("Total Volunteer Hours") && columns.Contains("POC ID"))
                {
                    EventsDataProcess(dtContent);
                }
                else if (columns.Contains("Event ID") && columns.Contains("Base Location") && columns.Contains("Event Name") 
                    && columns.Contains("Employee ID") && columns.Contains("Volunteer Hours"))
                {
                    AssociatesEnrolledToEventProcess(dtContent);
                }
            }
            return false;
        }

        static void AssociatesDataProcess(DataTable dtContent)
        {
            List<AssociateModel> modelList = new List<AssociateModel>();
            AssociateModel model;
            foreach(DataRow dr in dtContent.Rows)
            {
                model = new AssociateModel();
                model.ID = dr["Associate ID"] != null ? Convert.ToInt32(dr["Associate ID"]) : 0;
                model.Name = Convert.ToString(dr["Name"]);
                model.Designation = Convert.ToString(dr["Designation"]);
                model.Designation = Convert.ToString(dr["Location"]);
                model.Designation = Convert.ToString(dr["BU"]);
                modelList.Add(model);
            }
            //_unitOfWork.Associates.AddRange(modelList);
        }
        static void EventsDataProcess(DataTable dtContent)
        {

        }
        static void AssociatesEnrolledToEventProcess(DataTable dtContent)
        {

        }
    }
}
