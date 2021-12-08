using CODWER.RERU.Evaluation.Data.Entities.Files;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IStorageFileService
    {
        public Task<Guid> AddFile(AddFileDto dto);
        public Task<Guid> AddFile(string fileName, FileTypeEnum fileType, string type, byte[] content);
        public Task<Unit> RemoveFile(Guid fileId);
        public Task<FileDataDto> GetFile(Guid fileId);
    }
}
