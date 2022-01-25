using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Personal.Application.Services
{
    public interface IStorageFileService
    {
        public Task<int> AddFile(int contractorId, string fileName, string contentType, FileTypeEnum type, byte[] content);
        public Task<int> AddFile(AddFileDto dto);
        public Task<Unit> RemoveFile(int fileId);
        public Task<FileDataDto> GetFile(int fileId);
        public Task<Unit> SignFile(SignFileDto dto);
    }
}
