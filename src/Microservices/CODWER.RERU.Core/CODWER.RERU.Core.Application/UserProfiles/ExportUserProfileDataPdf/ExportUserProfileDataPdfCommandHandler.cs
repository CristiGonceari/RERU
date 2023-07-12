using CODWER.RERU.Core.Application.Services.Implementations;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.ExportUserProfileDataPdf
{
    public class ExportUserProfileDataPdfCommandHandler : IRequestHandler<ExportUserProfileDataPdfCommand, FileDataDto>
    {
        private readonly IExportUserProfileDataPdf _exportUserProfileDataPdf;

        public ExportUserProfileDataPdfCommandHandler(IExportUserProfileDataPdf exportUserProfileDataPdf)
        {
            _exportUserProfileDataPdf = exportUserProfileDataPdf;
        }

        public Task<FileDataDto> Handle(ExportUserProfileDataPdfCommand request, CancellationToken cancellationToken)
        {
            var data = _exportUserProfileDataPdf.ExportUserProfileDatasPdf(request.UserProfileId);

            return data;
        }
    }
}
