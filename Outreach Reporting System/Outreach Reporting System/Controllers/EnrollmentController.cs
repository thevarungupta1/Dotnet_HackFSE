using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
        public async Task<ActionResult<IEnumerable<Enrollments>>> GetAsync()
        {
            await OnPostExport();
           return Ok(_enrollmentProcessor.GetAll());
        }

        // GET api/GetWithRelatedData
        [HttpGet]
        [Route("GetEnrolledAssociates")]
        public ActionResult<IEnumerable<Enrollments>> GetEnrolledAssociates()
        {
            return Ok(_enrollmentProcessor.GetEnrolledAssociates());
        }
        // GET api/GetEnrolledAssociates
        [HttpGet]
        [Route("GetTopFrequentVolunteers")]
        public IActionResult GetTopFrequentVolunteers(int count)
        {
            return Ok(_enrollmentProcessor.GetTopFrequentVolunteers(count));
        }

        [HttpGet]
        [Route("GetYearlyVolunteersCount")]
        public IActionResult GetYearlyVolunteersCount(int years)
        {
            return Ok(_enrollmentProcessor.GetYearlyVolunteersCount(years));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IEnumerable<Enrollments> enrollments)
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
        [HttpGet("ExcelDownload")]
        public async Task<IActionResult> OnPostExport()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                sWebRootFolder = Path.Combine(Directory.GetCurrentDirectory(), "test");
            }
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write);
            
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Name");
                row.CreateCell(2).SetCellValue("Age");

                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Kane Williamson");
                row.CreateCell(2).SetCellValue(29);

                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Martin Guptil");
                row.CreateCell(2).SetCellValue(33);

                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Colin Munro");
                row.CreateCell(2).SetCellValue(23);

                workbook.Write(fs);
            MemoryStream memory = new MemoryStream();

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}
