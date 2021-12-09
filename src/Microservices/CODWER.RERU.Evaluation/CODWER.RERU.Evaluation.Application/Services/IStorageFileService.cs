﻿using CODWER.RERU.Evaluation.Data.Entities.Files;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IStorageFileService
    {
        public Task<string> AddFile(AddFileDto dto);
        public Task<string> AddFile(string fileName, FileTypeEnum fileType, string type, byte[] content);
        public Task<Unit> RemoveFile(string fileId);
        public Task<FileDataDto> GetFile(string fileId);
    }
}