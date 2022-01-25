using CODWER.RERU.Personal.DataTransferObjects.Instructions;
using MediatR;

namespace CODWER.RERU.Personal.Application.Instructions.UpdateInstruction
{
    public class UpdateInstructionCommand : IRequest<Unit>
    {
        public UpdateInstructionCommand(AddEditInstructionDto data)
        {
            Data = data;
        }

        public AddEditInstructionDto Data { get; set; }
    }
}
