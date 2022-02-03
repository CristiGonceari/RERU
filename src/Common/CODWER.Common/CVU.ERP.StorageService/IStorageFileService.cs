﻿using CVU.ERP.StorageService.Entities;
using CVU.ERP.StorageService.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;

namespace CVU.ERP.StorageService
{
    public interface IStorageFileService
    {
        public Task<string> AddFile(AddFileDto dto);
        public Task<string> AddFile(string fileName, FileTypeEnum fileType, string type, byte[] content);
        public Task<Unit> RemoveFile(string fileId);
        public Task<FileDataDto> GetFile(string fileId);
        public Task<List<File>> GetDemoList();
    }
}