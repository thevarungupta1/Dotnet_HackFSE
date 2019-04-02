using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Data.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Outreach.Reporting.Business.Processors
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileProcessor(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<File> GetAll()
        {
            try
            {
                return _unitOfWork.File.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool SaveFiles(IEnumerable<File> files)
        {
            try
            {
                foreach (var row in files)
                {
                    row.CreatedOn = DateTime.Now;
                }
                _unitOfWork.File.AddRange(files);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ReadExcelFile(string filePath)
        {
            bool result = false;
            try
            {
                var dtContent = GetDataTableFromExcel(filePath);
                result = DataTableProcess(dtContent);
            }
            catch(Exception ex)
            {

            }
            return result;
        }
        private DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = System.IO.File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                if (ws.Dimension != null && ws.Dimension.End != null)
                {
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                }
                return tbl;
            }
        }

        public bool DataTableProcess(DataTable dtContent)
        {
            bool result = false;
            try
            {

                if (dtContent.Rows != null && dtContent.Rows.Count > 0)
                {
                    var dr = dtContent.Rows[0];
                    DataColumnCollection columns = dtContent.Columns;
                    if (columns.Contains("Associate ID") && columns.Contains("Name") && columns.Contains("Designation"))
                    {
                        result = AssociatesDataProcess(dtContent);
                    }
                    else if (columns.Contains("Event ID") && columns.Contains("Venue Address") && columns.Contains("Event Name")
                        && columns.Contains("Total Volunteer Hours") && columns.Contains("POC ID"))
                    {
                        result = EventsDataProcess(dtContent);
                    }
                    else if (columns.Contains("Event ID") && columns.Contains("Base Location") && columns.Contains("Event Name")
                        && columns.Contains("Employee ID") && columns.Contains("Volunteer Hours"))
                    {
                        result = AssociatesEnrolledToEventProcess(dtContent);
                    }
                }
            }
            catch(Exception ex)
            {
                //return false;
            }
            return result;
        }

        private bool AssociatesDataProcess(DataTable dtContent)
        {
            List<Associate> modelList = new List<Associate>();
            Associate model;
            foreach (DataRow dr in dtContent.Rows)
            {
                model = new Associate();
                model.ID = dr["Associate ID"] != null ? Convert.ToInt32(dr["Associate ID"]) : 0;
                model.Name = Convert.ToString(dr["Name"]);
                model.Designation = Convert.ToString(dr["Designation"]);
                model.BaseLocation = Convert.ToString(dr["Location"]);
                model.BusinessUnit = Convert.ToString(dr["BU"]);
                model.CreatedOn = DateTime.Now;
                modelList.Add(model);
            }
            _unitOfWork.Associates.AddRange(modelList);
            return true;
        }
        private bool EventsDataProcess(DataTable dtContent)
        {
            List<Event> modelList = new List<Event>();
            Event model;
            foreach (DataRow dr in dtContent.Rows)
            {
                model = new Event();
                model.ID = Convert.ToString(dr["Event ID"]);
                model.Name = Convert.ToString(dr["Event Name"]);
                model.Description = Convert.ToString(dr["Event Description"]);
                model.Date = Convert.ToDateTime(dr["Event Date (DD-MM-YY)"]);
                model.TotalVolunteers = Convert.ToInt32(dr["Total no. of volunteers"]);
                model.TotalVolunteerHours = Convert.ToDecimal(dr["Total Volunteer Hours"]);
                model.TotalTravelHours = Convert.ToDecimal(dr["Total Travel Hours"]);
                model.LivesImpacted = Convert.ToInt32(dr["Lives Impacted"]);
                model.BaseLocation = Convert.ToString(dr["Base Location"]);
                string fullAddress = Convert.ToString(dr["Venue Address"]);
                if (!string.IsNullOrEmpty(fullAddress))
                {
                    var fullAddressList = fullAddress.Split(',');
                    if (fullAddressList.Length > 0)
                    {
                        if (fullAddressList.Length - 4 >= 0)
                            model.Address = fullAddressList[fullAddressList.Length - 4];
                        if (fullAddressList.Length - 3 >= 0)
                            model.City = fullAddressList[fullAddressList.Length - 3];
                        if (fullAddressList.Length - 2 >= 0)
                            model.State = fullAddressList[fullAddressList.Length - 2];
                        if (fullAddressList.Length - 1 > 0)
                        {
                            var countryWithPin = fullAddressList[fullAddressList.Length - 1];
                            if (!string.IsNullOrEmpty(countryWithPin))
                            {
                                var values = countryWithPin.Split('-');
                                if (values.Length - 1 > 0)
                                {
                                    model.Country = values[0];
                                    model.PinCode = values[1];
                                }
                                else
                                    model.PinCode = values[0];
                            }
                        }
                        model.Address = fullAddressList[0];
                    }
                }
                model.Beneficiary = Convert.ToString(dr["Beneficiary Name"]);
                model.CouncilName = Convert.ToString(dr["Council Name"]);
                model.Project = Convert.ToString(dr["Project"]);
                model.Category = Convert.ToString(dr["Category"]);
                model.ActivityType = Convert.ToInt32(dr["Activity Type"]);
                model.Status = Convert.ToString(dr["Status"]);
                model.CreatedOn = DateTime.Now;
                modelList.Add(model);
            }
            _unitOfWork.Events.AddRange(modelList);
            return true;
        }
        private bool AssociatesEnrolledToEventProcess(DataTable dtContent)
        {
            List<Enrollment> modelList = new List<Enrollment>();
            Enrollment model;
            foreach (DataRow dr in dtContent.Rows)
            {
                model = new Enrollment();
                model.AssociateID = Convert.ToInt32(dr["Employee ID"]);
                model.EventID = Convert.ToString(dr["Event ID"]);
                model.BaseLocation = Convert.ToString(dr["Base Location"]);
                model.BusinessUnit = Convert.ToString(dr["Business Unit"]);
                model.EventDate = Convert.ToDateTime(dr["Event Date (DD-MM-YY)"]);
                model.VolunteerHours = Convert.ToDecimal(dr["Volunteer Hours"]);
                model.TravelHours = Convert.ToInt32(dr["Travel Hours"]);
                model.Status = Convert.ToString(dr["Status"]);
                model.IIEPCategory = Convert.ToString(dr["IIEP Category"]);
                model.CreatedOn = DateTime.Now;
                modelList.Add(model);
            }
            _unitOfWork.Enrollments.AddRange(modelList);
            return true;
        }


    }
}
