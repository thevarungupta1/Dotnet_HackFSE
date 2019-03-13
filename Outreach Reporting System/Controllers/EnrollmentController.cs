using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, PMO, POC")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentProcessor _enrollmentProcessor;
        private readonly ILogger<EnrollmentController> _logger;
        private IHostingEnvironment _hostingEnvironment;
        public EnrollmentController(IEnrollmentProcessor enrollmentProcessor, ILogger<EnrollmentController> logger, IHostingEnvironment hostingEnvironment)
        {
            _enrollmentProcessor = enrollmentProcessor;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
     
       
        // GET api/AllEnrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAsync()
        {
           return Ok(_enrollmentProcessor.GetAll());
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public ActionResult<IEnumerable<Associate>> GetEnrolledAssociates()
        {
            return Ok(_enrollmentProcessor.GetEnrolledAssociates());
        }

        [HttpGet]
        [Route("GetEnrolledUniqueAssociates")]
        public ActionResult<IEnumerable<Associate>> GetEnrolledUniqueAssociates()
        {
            return Ok(_enrollmentProcessor.GetEnrolledUniqueAssociates());
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetTopFrequentVolunteers")]
        public IActionResult GetTopFrequentVolunteers(int count)
        {
            return Ok(_enrollmentProcessor.GetTopFrequentVolunteers(count));
        }

        [HttpGet]
        [Route("GetTopVolunteerData")]
        public IActionResult GetTopVolunteerData()
        {
            return Ok(_enrollmentProcessor.GetTopVolunteerData());
        }

        [HttpGet]
        [Route("GetYearlyVolunteersCount")]
        public IActionResult GetYearlyVolunteersCount(int years)
        {
            return Ok(_enrollmentProcessor.GetYearlyVolunteersCount(years));
        }
        [HttpGet]
        [Route("GetYearlyBuWiseVolunteersCount")]
        public IActionResult GetYearlyBuWiseVolunteersCount(int years)
        {
            return Ok(_enrollmentProcessor.GetYearlyBuWiseVolunteersCount(years));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersCount")]
        public IActionResult GetDesignationWiseVolunteersCount()
        {
            return Ok(_enrollmentProcessor.GetDesignationWiseVolunteersCount());
        }

        [HttpGet]
        [Route("GetAllNewVolunteers")]
        public IActionResult GetAllNewVolunteers()
        {
            return Ok(_enrollmentProcessor.GetAllNewVolunteers());
        }

        [HttpGet]
        [Route("GetDateWiseVolunteersCount")]
        public IActionResult GetDateWiseVolunteersCount()
        {
            return Ok(_enrollmentProcessor.GetDateWiseVolunteersCount());
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IEnumerable<Enrollment> enrollments)
        {
            _enrollmentProcessor.SaveEnrollments(enrollments);
        }
       
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
       
        [HttpGet]
        [Route("ExcelExport")]
        public IActionResult ExcelExport(string fileName="Report Data")
        {
            DataTable table = new DataTable();
            //Fill datatable
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(fileName);
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                fileContents = package.GetAsByteArray();
            }
            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: fileName + ".xlsx"
            );
        }
    }
}
