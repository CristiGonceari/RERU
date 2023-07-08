using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.ExportUserProfileDataPdf
{
    public class ExportUserProfileDataPdfCommand : IRequest<FileDataDto>
    {
        public int UserProfileId { get; set; }
    }
}
