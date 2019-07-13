﻿using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class FileController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(FileController));

        public FileController(IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor;
        }
        // GET api/File
        [HttpGet]
        public async Task<ActionResult<IEnumerable<File>>> Get()
        {
            return await Task.FromResult(Ok(_fileProcessor.GetAll()));
        }
        
        // POST api/File
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<File> files)
        {
            if(files == null)
                return BadRequest();
            return await Task.FromResult(Ok( _fileProcessor.SaveFiles(files)));
        }
        
        [HttpGet]
        [Route("ExcelExport")]
        public async Task<IActionResult> ExcelExport(string fileName = "Report Data")
        {
            DataTable table = new DataTable();

            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(fileName);
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                fileContents = package.GetAsByteArray();
            }
            return await Task.FromResult(File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: fileName + ".xlsx"
            ));
        }

        // POST api/File
        [HttpPost]
        [Route("ReadExcel")]
        public async Task<IActionResult> ReadExcelFromPath([FromBody]string filePath)
        {
            if(string.IsNullOrEmpty(filePath))
                return BadRequest();
            return await Task.FromResult(Ok(_fileProcessor.ReadExcelFile(filePath)));
        }
    }
}