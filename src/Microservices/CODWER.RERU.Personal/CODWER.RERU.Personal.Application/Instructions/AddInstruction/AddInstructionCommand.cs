
using CODWER.RERU.Personal.DataTransferObjects.Instructions;
using MediatR;

namespace CODWER.RERU.Personal.Application.Instructions.AddInstruction
{
    public class AddInstructionCommand : IRequest<int>
    {
        public AddInstructionCommand(AddEditInstructionDto data)
        {
            Data = data;
        }

        public AddEditInstructionDto Data { get; set; }
    }
}
