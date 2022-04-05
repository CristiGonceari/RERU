using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using CVU.ERP.StorageService.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CloudFileController : BaseController
    {
        private readonly IStorageFileService _storageFileService;
        public CloudFileController(IStorageFileService storageFileService, IMediator mediator) : base(mediator)
        {
            _storageFileService = storageFileService;
        }

        [HttpPost]
        [RequestSizeLimit(270_000_000)]
        public async Task<string> UploadFile([FromForm] AddFileDto dto)
        {
            return await _storageFileService.AddFile(dto);
        }

        [HttpGet("{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile([FromRoute] string fileId)
        {
            var result = await _storageFileService.GetFile(fileId);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpDelete("{fileId}")]
        public async Task<Unit> DeleteFile([FromRoute] string fileId)
        {
            return await _storageFileService.RemoveFile(fileId);
        }

        [HttpGet("allFiles")]
        public async Task<List<File>> GetDemoFiles()
        {
            return await _storageFileService.GetDemoList();
        }
    }
}
