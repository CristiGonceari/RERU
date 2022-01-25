using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Personal.Application.Services.Implementations.Storage
{
    public class CloudStorageFileService : IStorageFileService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly string _folderPath;

        public CloudStorageFileService(AppDbContext appDbContext, IUserProfileService userProfileService, IOptions<DocumentOptions> options)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _folderPath = options.Value.Location;
        }

        public async Task<int> AddFile(int contractorId, string fileName, string contentType, FileTypeEnum type, byte[] content)
        {
            try
            {
                var prefix = GetUniqueFilePrefix();
                var uniqueFileName = $"{prefix}_{fileName}";
                var fileFullPath = $"{_folderPath}/{uniqueFileName}";
            
                await File.WriteAllBytesAsync(fileFullPath, content);

                var fileToAdd = new ByteArrayFile
                {
                    ContractorId = contractorId,
                    FileName = fileName,
                    ContentType = contentType,
                    Type = type,
                    UniqueFileName = uniqueFileName
                };

                await _appDbContext.ByteFiles.AddAsync(fileToAdd);
                await _appDbContext.SaveChangesAsync();

                return fileToAdd.Id;
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR ADD: {e.Message}");
            }
        }

        public async Task<int> AddFile(AddFileDto dto)
        {
            try
            {
                var prefix = GetUniqueFilePrefix();
                var uniqueFileName = $"{prefix}_{dto.File.FileName}";
                var fileFullPath = $"{_folderPath}/{uniqueFileName}";

                await using var ms = new MemoryStream();
                await dto.File.CopyToAsync(ms);
                await File.WriteAllBytesAsync(fileFullPath, ms.ToArray());

                var fileToAdd = new ByteArrayFile
                {
                    ContractorId = dto.ContractorId,
                    FileName = dto.File.FileName,
                    ContentType = dto.File.ContentType,
                    Type = dto.Type,
                    UniqueFileName = uniqueFileName
                };

                await _appDbContext.ByteFiles.AddAsync(fileToAdd);
                await _appDbContext.SaveChangesAsync();

                return fileToAdd.Id;
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR ADD: {e.Message}");
            }
        }

        public async Task<Unit> RemoveFile(int fileId)
        {
            try
            {
                var toRemove = await _appDbContext.ByteFiles.FirstOrDefaultAsync(x => x.Id == fileId);

                if (toRemove != null)
                {
                    var path = $"{_folderPath}/{toRemove.UniqueFileName}";
                    File.Delete(path);

                    _appDbContext.ByteFiles.Remove(toRemove);
                    await _appDbContext.SaveChangesAsync();
                }

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR REMOVE: {e.Message}");
            }
        }

        public async Task<FileDataDto> GetFile(int fileId)
        {
            try
            {
                var dbFile = await _appDbContext.ByteFiles.FirstOrDefaultAsync(x => x.Id == fileId);

                if (dbFile != null)
                {
                    var path = $"{_folderPath}/{dbFile.UniqueFileName}";
                    var bytes = await File.ReadAllBytesAsync(path);

                    return new FileDataDto
                    {
                        Content = bytes,
                        ContentType = dbFile.ContentType,
                        Name = dbFile.FileName
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR GET: {e.Message}");
            }

            return null;
        }

        private string GetUniqueFilePrefix()
        {
            string guidString;

            do
            {
                Guid guid = Guid.NewGuid();
                guidString = Convert.ToBase64String(guid.ToByteArray());
                guidString = guidString
                    .Replace("\\", "")
                    .Replace("/", "")
                    .Replace(":", "")
                    .Replace("*", "")
                    .Replace("?", "")
                    .Replace("\"", "")
                    .Replace(">", "")
                    .Replace("|", "")
                    .Replace("=", "")
                    .Replace("+", "");

            } while (_appDbContext.ByteFiles.Any(f => f.UniqueFileName.Contains(guidString)));

            return guidString;
        }
    }
}
