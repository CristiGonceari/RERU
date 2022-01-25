using MediatR;

namespace CODWER.RERU.Personal.Application.Instructions.RemoveInstruction
{
    public class RemoveInstructionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
