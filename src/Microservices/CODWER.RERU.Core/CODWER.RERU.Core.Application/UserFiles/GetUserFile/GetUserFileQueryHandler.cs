using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserFiles.GetUserFile
{
    public class GetUserFileQueryHandler : BaseHandler, IRequestHandler<GetUserFileQuery, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;

        public GetUserFileQueryHandler(ICommonServiceProvider commonServiceProvider, 
            IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _storageFileService = storageFileService;
        }

        public async Task<FileDataDto> Handle(GetUserFileQuery request, CancellationToken cancellationToken)
        {
            return await _storageFileService.GetFile(request.FileId);
        }
    }
}
