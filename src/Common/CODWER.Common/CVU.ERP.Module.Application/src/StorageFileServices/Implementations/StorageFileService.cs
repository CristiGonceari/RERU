using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using CVU.ERP.StorageService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minio;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = CVU.ERP.StorageService.Entities.File;

namespace CVU.ERP.Module.Application.StorageFileServices.Implementations
{
    public class StorageFileService : IStorageFileService
    {
        private readonly StorageDbContext _appDbContext;
        private readonly MinioClient _minio;

        public StorageFileService(IOptions<MinioSettings> fileOptions, StorageDbContext storageDbContext)
        {
            _appDbContext = storageDbContext;
            _minio = new MinioClient(fileOptions.Value.Endpoint, fileOptions.Value.AccessKey, fileOptions.Value.SecretKey); ;
        }

        public async Task<string> AddFile(AddFileDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var prefix = GetUniqueFilePrefix();
                    var uniqueFileName = $"{prefix}_{dto.File.FileName}";

                    await using var ms = new MemoryStream();
                    await dto.File.CopyToAsync(ms);

                    await CreateBucket(dto.Type.ToString());
                    await FileUpload(dto.Type.ToString(), uniqueFileName, ms.ToArray());

                    var fileToAdd = new File
                    {
                        Id = new Guid(),
                        FileName = dto.File.FileName,
                        FileType = dto.Type,
                        UniqueFileName = uniqueFileName,
                        BucketName = dto.Type.ToString(),
                        Type = dto.File.ContentType
                    };

                    await using var db = _appDbContext.NewInstance();
                    await db.Files.AddAsync(fileToAdd);
                    await db.SaveChangesAsync();

                    return fileToAdd.Id.ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR ADD: {e.Message}");
            }

            return string.Empty;
        }

        public async Task<string> AddFile(string fileName, FileTypeEnum fileType, string type, byte[] content)
        {
            try
            {
                var prefix = GetUniqueFilePrefix();
                var uniqueFileName = $"{prefix}_{fileName}";

                await CreateBucket(fileType.ToString());
                await FileUpload(fileType.ToString(), uniqueFileName, content);

                var fileToAdd = new File
                {
                    Id = new Guid(),
                    FileName = fileName,
                    Type = type,
                    FileType = fileType,
                    UniqueFileName = uniqueFileName,
                    BucketName = fileType.ToString()
                };

                await using var db = _appDbContext.NewInstance();
                await db.Files.AddAsync(fileToAdd);
                await db.SaveChangesAsync();

                return fileToAdd.Id.ToString();
            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR ADD: {e.Message}");
            }
        }

        public async Task<Unit> RemoveFile(string fileId)
        {
            try
            {
                await using var db = _appDbContext.NewInstance();

                var toRemove = await db.Files.FirstOrDefaultAsync(x => x.Id.ToString() == fileId);

                if (toRemove != null)
                {
                    await _minio.RemoveObjectAsync(toRemove.BucketName, toRemove.UniqueFileName);

                    db.Files.Remove(toRemove);
                    await db.SaveChangesAsync();
                }

                return Unit.Value;

            }
            catch (Exception e)
            {
                throw new Exception($"FILE ERROR REMOVE: {e.Message}");
            }
        }

        public async Task<FileDataDto> GetFile(string fileId)
        {
            try
            {
                await using var db = _appDbContext.NewInstance();

                var dbFile = await db.Files.FirstOrDefaultAsync(x => x.Id.ToString() == fileId);

                if (dbFile != null)
                {
                    var fileStream = new MemoryStream();

                    await _minio.GetObjectAsync(dbFile.BucketName, dbFile.UniqueFileName,
                                                s =>
                                                {
                                                    s.CopyTo(fileStream);
                                                }
                                            );

                    return FileDataDto.GetByType(dbFile.FileName, fileStream.ToArray(), dbFile.Type);
                }
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }

            return null;
        }

        public async Task<List<File>> GetDemoList()
        {
            await using var db = _appDbContext.NewInstance();
            var list = db.Files.ToList();
            return list;
        }

        public async Task<string> GetFileName(string fileId)
        {
            await using var db = _appDbContext.NewInstance();
            var name = db.Files.FirstOrDefault(x => x.Id.ToString() == fileId).FileName;
            
            return name;
        }

        public async Task<IQueryable<File>> GetUserFiles(List<string> fileIdList)
        {
            await using var db = _appDbContext.NewInstance();
            var results = db.Files.Where(file => fileIdList.Contains(file.Id.ToString())).ToList();

            return results.AsQueryable();
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
