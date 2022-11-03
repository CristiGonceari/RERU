using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.ExportUserProfileData
{
    public class ExportUserProfileDataCommand : IRequest<FileDataDto>
    {
        public int UserProfileId { get; set; }
    }

}
