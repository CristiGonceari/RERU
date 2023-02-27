using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserFiles.PrintUserFiles
{
    public class PrintUserFilesCommandHandler : BaseHandler, IRequestHandler<PrintUserFilesCommand, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;
        private readonly IExportData<File, GetFilesDto> _printer;

        public PrintUserFilesCommandHandler(ICommonServiceProvider commonServiceProvider,
            IStorageFileService storageFileService,
            IExportData<File, GetFilesDto> printer) : base(commonServiceProvider)
        {
            _storageFileService = storageFileService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserFilesCommand request, CancellationToken cancellationToken)
        {

            var fileIdList = AppDbContext.UserFiles
                .Where(x => x.UserProfileId == request.UserProfileId)
                .Select(x => x.FileId).ToList();

            var files = await _storageFileService.GetUserFiles(fileIdList);

            var result = _printer.ExportTableSpecificFormat(new TableData<File>
            {
                Name = request.TableName,
                Items = files,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
