using Elsa;
using ElsaQuickstarts.Server.Dashboard.Models;
using ElsaQuickstarts.Server.Dashboard.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElsaQuickstarts.Server.Dashboard.Controllers
{
    [ApiController]
    [Route("files")]
    public class FilesController : Controller
    {
        private readonly IFileReceivedInvoker _invoker;

        public FilesController(IFileReceivedInvoker invoker)
        {
            _invoker = invoker;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Handle(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file");

            var fileModel = new FileModel
            {
                FileName = Path.GetFileName(file.FileName),
                Content = await file.OpenReadStream().ReadBytesToEndAsync(),
                MimeType = file.ContentType
            };

            var collectedWorkflows = await _invoker.DispatchWorkflowsAsync(fileModel);
            return Ok(collectedWorkflows.ToList());
        }
    }
}
