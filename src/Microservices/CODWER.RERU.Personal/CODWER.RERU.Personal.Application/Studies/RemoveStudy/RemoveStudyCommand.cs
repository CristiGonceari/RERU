using MediatR;

namespace CODWER.RERU.Personal.Application.Studies.RemoveStudy
{
    public class RemoveStudyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
