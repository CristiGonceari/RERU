using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using CVU.ERP.StorageService.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GenerateTestResultFile
{
    public class GenerateTestResultFileCommandHandler : IRequestHandler<GenerateTestResultFileCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;
        private readonly IPdfService _pdfService;

        public GenerateTestResultFileCommandHandler(AppDbContext appDbContext, IStorageFileService storageFileService, IPdfService pdfService)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
            _pdfService = pdfService;
        }

        public async Task<int> Handle(GenerateTestResultFileCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t  => t.DocumentsForSign)
                .ThenInclude(dfs => dfs.SignedDocuments)
                .Select(t => new Test 
                {
                    Id = t.Id,
                    TestTemplate =  new TestTemplate { Name = t.TestTemplate.Name },
                    UserProfile = new UserProfile { Idnp = t.UserProfile.Idnp },
                    DocumentsForSign = t.DocumentsForSign,
                    TestStatus = t.TestStatus
                })
                .FirstOrDefaultAsync(t => t.Id == request.TestId);

            var testResultPdf = await _pdfService.PrintTestResultPdf(request.TestId);
            testResultPdf.Name = testResultPdf.Name.Replace("Test_Result", (test.TestTemplate.Name.Replace(".", "/") + " (" + test.UserProfile.Idnp.Replace("\n", "") + ")").ToString());

            if (test.DocumentsForSign.Count() == 0 && test.TestStatus == TestStatusEnum.Verified)
            {
                var file = new AddFileDto
                {
                    File = ConvertToIFormFile(testResultPdf),
                    Type = FileTypeEnum.documents
                };

                var storageFileId = await _storageFileService.AddFile(file);

                var document = new DocumentsForSign()
                {
                    FileName = testResultPdf.Name,
                    FileType = testResultPdf.ContentType,
                    MediaFileId = storageFileId,
                    TestId = request.TestId
                };

                await _appDbContext.DocumentsForSign.AddAsync(document);
                await _appDbContext.SaveChangesAsync();

                return document.Id;
            }

            return 0;
        }

        public static IFormFile ConvertToIFormFile(FileDataDto dtoFile)
        {
            byte[] fileBytes = Convert.FromBase64String(Convert.ToBase64String(dtoFile.Content)); // assuming the content is base64 encoded
            MemoryStream stream = new MemoryStream(fileBytes);

            IFormFile formFile = new FormFile(stream, 0, stream.Length, dtoFile.Name, dtoFile.Name)
            {
                Headers = new HeaderDictionary
                {
                    { "Content-Type", dtoFile.ContentType }
                }
            };

            return formFile;
        }
    }
}
