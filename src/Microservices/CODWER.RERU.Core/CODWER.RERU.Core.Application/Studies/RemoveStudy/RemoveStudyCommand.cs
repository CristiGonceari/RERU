using MediatR;

namespace CODWER.RERU.Core.Application.Studies.RemoveStudy
{
    public class RemoveStudyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
