using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.UpdateUserProfileGeneralData
{
    public class UpdateUserProfileGeneralDataCommand : IRequest<Unit>
    {
        public UpdateUserProfileGeneralDataCommand(UserProfileGeneralDataDto data)
        {
            Data = data;
        }

        public UserProfileGeneralDataDto Data { get; set; }
    }
}
