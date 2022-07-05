using CODWER.RERU.Core.DataTransferObjects.Users;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.EditCandidate
{
    public class EditCandidateCommand : IRequest<int>
    {
        public EditCandidateDto Data { get; set; }
    }
}
