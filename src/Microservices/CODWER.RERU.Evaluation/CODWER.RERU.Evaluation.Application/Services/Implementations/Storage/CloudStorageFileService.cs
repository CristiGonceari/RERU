using CODWER.RERU.Evaluation.Data.Entities.Files;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations.Storage
{
    public class CloudStorageFileService : IStorageFileService
    {
        private readonly AppDbContext _appDbContext;
        private readonly MinioClient _minio;

        public CloudStorageFileService(AppDbContext appDbContext, IOptions<DataTransferObjects.Files.FileOptions> fileOptions) {

            _appDbContext = appDbContext;
            _minio = new MinioClient(fileOptions.Value.endpoint, fileOptions.Value.accessKey, fileOptions.Value.secretKey);

        }

        public async Task<Guid> AddFile(string fileName, FileTypeEnum fileType, string type, byte[] content)
        {
            try
            {
                var prefix = GetUniqueFilePrefix();
                var uniqueFileName = $"{prefix}_{fileName}";

                await CreateBucket(fileType.ToString());
                await FileUpload(fileType.ToString(), uniqueFileName, content);

                var fileToAdd = new Data.Entities.Files.File
                {
                    FileName = fileName,
                    Type = type,
                    FileType = fileType,
                    UniqueFileName = uniqueFileName,
                    BucketName = fileType.ToString()
                };

                await _appDbContext.Files.AddAsync(fileToAdd);
                await _appDbContext.SaveChangesAsync();

                return fileToAdd.Id;
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR ADD: {e.Message}");
            }
        }

        public async Task<Guid> AddFile(AddFileDto dto)
        {
            try
            {
                var prefix = GetUniqueFilePrefix();
                var uniqueFileName = $"{prefix}_{dto.File.FileName}";

                await using var ms = new MemoryStream();
                await dto.File.CopyToAsync(ms);

                await CreateBucket(dto.Type.ToString());
                await FileUpload(dto.Type.ToString(), uniqueFileName, ms.ToArray());

                var fileToAdd = new Data.Entities.Files.File
                {
                    FileName = dto.File.FileName,
                    FileType = dto.Type,
                    UniqueFileName = uniqueFileName,
                    BucketName = dto.Type.ToString(),
                    Type = dto.File.ContentType
                };

                await _appDbContext.Files.AddAsync(fileToAdd);
                await _appDbContext.SaveChangesAsync();

                return fileToAdd.Id;
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR ADD: {e.Message}");
            }
        }

        public async Task<Unit> RemoveFile(Guid fileId)
        {
            try
            {
                var toRemove = await _appDbContext.Files.FirstOrDefaultAsync(x => x.Id == fileId);

                if (toRemove != null)
                {
                    var deleteObject = _minio.RemoveObjectAsync(toRemove.BucketName, toRemove.UniqueFileName);

                    _appDbContext.Files.Remove(toRemove);
                    await _appDbContext.SaveChangesAsync();
                }

                return Unit.Value;

            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR REMOVE: {e.Message}");
            }
        }

        public async Task<FileDataDto> GetFile(Guid fileId)
        {
            try
            {
                var dbFile = await _appDbContext.Files.FirstOrDefaultAsync(x => x.Id == fileId);

                if (dbFile != null)
                {   
                    var filestream = new MemoryStream();

                    await _minio.GetObjectAsync(dbFile.BucketName,
                                                    dbFile.UniqueFileName, 
                                                    s =>
                                                    {
                                                        s.CopyTo(filestream); 
                                                    }
                                                );

                     return new FileDataDto
                        {
                            Content = filestream.ToArray(),
                            ContentType = dbFile.Type,
                            Name = dbFile.FileName
                        }; 

                }
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }

            return null;
        }

        private async Task FileUpload(string bucketName, string objectName, byte[] ms)
        {
            try
            {
                var filestream = new MemoryStream(ms);

                await _minio.PutObjectAsync(bucketName, objectName, filestream, filestream.Length);
                Console.WriteLine("Successfully uploaded " + objectName);
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }

        private async Task CreateBucket(string bucketName)
        {
            bool found = await _minio.BucketExistsAsync(bucketName);

            if (!found)
            {
                await _minio.MakeBucketAsync(bucketName);
            }
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

            } while (_appDbContext.Files.Any(f => f.UniqueFileName.Contains(guidString)));

            return guidString;
        }

    }
}

