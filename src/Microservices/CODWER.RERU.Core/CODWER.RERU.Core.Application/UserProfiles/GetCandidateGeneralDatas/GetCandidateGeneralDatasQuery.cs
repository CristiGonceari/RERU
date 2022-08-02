using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateGeneralDatas
{
    public class GetCandidateGeneralDatasQuery : IRequest<EditCandidateDto>
    {
        public GetCandidateGeneralDatasQuery(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}
