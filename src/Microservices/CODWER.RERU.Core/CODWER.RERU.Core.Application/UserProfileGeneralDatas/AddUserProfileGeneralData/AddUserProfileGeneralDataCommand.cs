using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.AddUserProfileGeneralData
{
    public class AddUserProfileGeneralDataCommand : IRequest<int>
    {
        public AddUserProfileGeneralDataCommand(UserProfileGeneralDataDto data)
        {
            Data = data;
        }
        public UserProfileGeneralDataDto Data { get; set; }
    }
}
