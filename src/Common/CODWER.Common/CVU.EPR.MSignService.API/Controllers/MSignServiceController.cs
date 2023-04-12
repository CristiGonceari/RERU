using Age.Integrations.MSign.Soap;
using CVU.ERP.ServiceProvider;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using CVU.ERP.StorageService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CVU.ERP.MSignService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MSignServiceController : Controller
    {
        private readonly IMSignSoapClient _mSignClient;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;


        public MSignServiceController(
            IMSignSoapClient mSignClient,
            ICurrentApplicationUserProvider currentUserProvider, 
            AppDbContext appDbContext,
            IStorageFileService storageFileService
            )
        {
            _mSignClient = mSignClient;
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }

        [HttpGet("SignDocument/{docId}")]
        public async Task<string> Challenge([FromRoute] int docId)
        {
            var documentForSign = await _appDbContext.DocumentsForSign.Include(dfs => dfs.SignedDocuments).FirstOrDefaultAsync(dfs => dfs.Id == docId);

            if (documentForSign == null)
            {
                throw new Exception("Invalid return document id");
            }

            var currentUser = await _currentUserProvider.Get();

            var signedDocument = documentForSign.SignedDocuments?.FirstOrDefault(sd => sd.UserProfileId == Int32.Parse(currentUser.Id));

            if (signedDocument?.Status == SignRequestStatusEnum.Success)
            {
                throw new Exception("Already sign by this user");
            }

            var file = await _storageFileService.GetFile(documentForSign.MediaFileId);

            var signRequest = new SignRequest()
            {
                ContentDescription = $"{file.Name}",
                ContentType = ContentType.Pdf,
                Contents = new SignContent[]{
                    new SignContent()
                    {
                        Content = file.Content,
                        Name= $"{file.Name}"
                    }
                },
                ShortContentDescription = $"{file.Name}",
                SignatureReason = "Semnarea datelor",
                ExpectedSigner = new ExpectedSigner()
                {
                    ID = currentUser.Idnp
                }
            };

            var signRequestId = await _mSignClient.PostSignRequestAsync(signRequest);

            if (signRequestId != null && signedDocument == null)
            {
                var signedDoc = new SignedDocument()
                {
                    DocumentsForSignId = docId,
                    UserProfileId = Int32.Parse(currentUser.Id),
                    SignRequestId = signRequestId,
                    Status = SignRequestStatusEnum.Pending
                };

                await _appDbContext.SignedDocuments.AddAsync(signedDoc);

                await _appDbContext.SaveChangesAsync();
            }
            else if (signRequestId != null && signedDocument != null)
            {
                signedDocument.SignRequestId = signRequestId.ToString();

                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("MSign server error");
            }

             return signRequestId;
        }

        //For localhost
        //[HttpPost("MSignCallback/{requestId}")]

        [HttpGet("MSignCallback/{requestId}")]
        public async Task<IActionResult> CheckMSingResponse([FromRoute] string requestId, [FromQuery]string redirectUrl)
       {
            redirectUrl = redirectUrl.Replace("@", "/").Replace("$", "#");

            var response = await _mSignClient.GetSignResponseAsync(requestId, "ro");

            if (response.Status.ToString() == SignRequestStatusEnum.Success.ToString())
            {
                var signedDoc = await _appDbContext.SignedDocuments.Include(sd => sd.DocumentsForSign).FirstOrDefaultAsync(sd => sd.SignRequestId == requestId);

                byte[] fileBytes = Convert.FromBase64String(Convert.ToBase64String(response.Results[0].Signature));
                MemoryStream stream = new MemoryStream(fileBytes);
                IFormFile formFile = new FormFile(stream, 0, stream.Length, signedDoc.DocumentsForSign.FileName, signedDoc.DocumentsForSign.FileName)
                {
                    Headers = new HeaderDictionary
                    {
                        { "Content-Type", signedDoc.DocumentsForSign.FileType }
                    }
                };

                var newSignedFile = new AddFileDto()
                {
                    File = formFile,
                    Type = FileTypeEnum.documents
                };

                var fileToAdd = await _storageFileService.AddFile(newSignedFile);
                await _storageFileService.RemoveFile(signedDoc.DocumentsForSign.MediaFileId);

                signedDoc.Status = SignRequestStatusEnum.Success;
                signedDoc.DocumentsForSign.MediaFileId = fileToAdd;

                await _appDbContext.SaveChangesAsync();

            }

            return Redirect(redirectUrl);
        }
    }
}
