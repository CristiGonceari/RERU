using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetPersonalData
{
    public class GetPersonalDataQuery : IRequest<UserPersonalDataDto>
    {
    }
}
