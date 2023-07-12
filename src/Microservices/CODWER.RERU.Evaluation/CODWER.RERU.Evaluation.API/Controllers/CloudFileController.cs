﻿using CODWER.RERU.Evaluation.API.Config;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using CVU.ERP.StorageService.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CloudFileController : BaseController
    {
        private readonly IStorageFileService _storageFileService;
        public CloudFileController(IStorageFileService iStorageFileService)
        {

            _storageFileService = iStorageFileService;
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

            string normalizedString = result.Name.Normalize(NormalizationForm.FormD);
            string removedFileDiacritics = new string(normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());

            return File(result.Content, result.ContentType, removedFileDiacritics);
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
