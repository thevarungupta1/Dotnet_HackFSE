using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
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
            int userId = GetCurrentUserId();
          return await Task.FromResult( Ok(_enrollmentProcessor.GetAll(userId)));
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public async Task<ActionResult<IEnumerable<Associate>>> GetEnrolledAssociates()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrolledAssociates(userId)));
        }

        [HttpGet]
        [Route("GetEnrolledUniqueAssociates")]
        public async Task<ActionResult<IEnumerable<Associate>>> GetEnrolledUniqueAssociates()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetEnrolledUniqueAssociates(userId)));
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetTopFrequentVolunteers")]
        public async Task<IActionResult> GetTopFrequentVolunteers(int count)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetTopFrequentVolunteers(count, userId)));
        }

        [HttpGet]
        [Route("GetTopVolunteerData")]
        public async Task<IActionResult> GetTopVolunteerData()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetTopVolunteerData(userId)));
        }

        [HttpGet]
        [Route("GetYearlyVolunteersCount")]
        public async Task<IActionResult> GetYearlyVolunteersCount(int years)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetYearlyVolunteersCount(years, userId)));
        }
        [HttpGet]
        [Route("GetYearlyBuWiseVolunteersCount")]
        public async Task<IActionResult> GetYearlyBuWiseVolunteersCount(int years)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetYearlyBuWiseVolunteersCount(years, userId)));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersCount")]
        public async Task<IActionResult> GetDesignationWiseVolunteersCount()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDesignationWiseVolunteersCount(userId)));
        }

        [HttpGet]
        [Route("GetDesignationWiseVolunteersByYear")]
        public async Task<IActionResult> GetDesignationWiseVolunteersByYear(int years)
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDesignationWiseNewRepeatedVolunteersCountByYear(years, userId)));
        }

        [HttpGet]
        [Route("GetAllNewVolunteers")]
        public async Task<IActionResult> GetAllNewVolunteers()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetAllNewVolunteers(userId)));
        }

        [HttpGet]
        [Route("GetDateWiseVolunteersCount")]
        public async Task<IActionResult> GetDateWiseVolunteersCount()
        {
            int userId = GetCurrentUserId();
            return await Task.FromResult(Ok(_enrollmentProcessor.GetDateWiseVolunteersCount(userId)));
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Enrollment> enrollments)
        {
           return await Task.FromResult(Ok( _enrollmentProcessor.SaveEnrollments(enrollments)));
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
        public async Task<IActionResult> ExcelExport(string fileName="Report Data")
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
            return await Task.FromResult( File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: fileName + ".xlsx"
            ));
        }

        private int GetCurrentUserId()
        {
            int userId = 0;
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    if (identity.FindFirst(ClaimTypes.Role).Value == "POC")
                        int.TryParse(identity.FindFirst("UserId").Value, out userId);
                }
            }
            catch (Exception ex)
            {

            }
            return userId;
        }
    }
}
