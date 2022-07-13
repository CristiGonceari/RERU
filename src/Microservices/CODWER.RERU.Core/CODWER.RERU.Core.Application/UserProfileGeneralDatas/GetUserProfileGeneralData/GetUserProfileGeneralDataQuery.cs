using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.GetUserProfileGeneralData
{
    public class GetUserProfileGeneralDataQuery : IRequest<UserProfileGeneralDataDto>
    {
        public int UserProfileId { get; set; }
    }
}
