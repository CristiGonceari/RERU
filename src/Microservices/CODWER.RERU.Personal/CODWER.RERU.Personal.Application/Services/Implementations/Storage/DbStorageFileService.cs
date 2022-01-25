using System.IO;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Services.Implementations.Storage
{
    public class DbStorageFileService : IStorageFileService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public DbStorageFileService(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<int> AddFile(int contractorId, string fileName, string contentType, FileTypeEnum type, byte[] content)
        {
            var fileToAdd = new ByteArrayFile
            {
                ContractorId = contractorId,
                FileName = fileName,
                ContentType = contentType,
                Type = type,
                Content = content
            };

            _appDbContext.ByteFiles.Add(fileToAdd);
            await _appDbContext.SaveChangesAsync();

            return fileToAdd.Id;
        }

        public async Task<int> AddFile(AddFileDto dto)
        {
            await using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);

            var fileToAdd = new ByteArrayFile
            {
                ContractorId = dto.ContractorId,
                FileName = dto.File.FileName,
                ContentType = dto.File.ContentType,
                Type = dto.Type,
                Content = ms.ToArray()
            };

            _appDbContext.ByteFiles.Add(fileToAdd);
            await _appDbContext.SaveChangesAsync();

            return fileToAdd.Id;
        }

        public async Task<Unit> RemoveFile(int fileId)
        {
            var toRemove = await _appDbContext.ByteFiles.FirstOrDefaultAsync(x => x.Id == fileId);

            if (toRemove != null)
            {
                _appDbContext.ByteFiles.Remove(toRemove);
                await _appDbContext.SaveChangesAsync();
            }
           
            return Unit.Value;
        }

        public async Task<FileDataDto> GetFile(int fileId)
        {
            var dbFile = await _appDbContext.ByteFiles.FirstOrDefaultAsync(x => x.Id == fileId);

            if (dbFile != null)
            {
                return new FileDataDto
                {
                    Content = dbFile.Content,
                    ContentType = dbFile.ContentType,
                    Name = dbFile.FileName
                };
            }

            return null;
        }
    }
}
