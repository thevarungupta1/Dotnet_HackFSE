using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Outreach.Reporting.Business.Interfaces;
using Outreach.Reporting.Entity.Entities;

namespace Outreach.Reporting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileProcessor fileProcessor, ILogger<FileController> logger)
        {
            _fileProcessor = fileProcessor;
            _logger = logger;
        }
        // GET api/File
        [HttpGet]
        public async Task<ActionResult<IEnumerable<File>>> Get()
        {
            return await Task.FromResult(Ok(_fileProcessor.GetAll()));
        }

        // GET api/File/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/File
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IEnumerable<File> files)
        {
            return await Task.FromResult(Ok( _fileProcessor.SaveFiles(files)));
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
    }
}