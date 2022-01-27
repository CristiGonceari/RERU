using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Instructions.AddInstruction;
using CODWER.RERU.Personal.Application.Instructions.RemoveInstruction;
using CODWER.RERU.Personal.Application.Instructions.UpdateInstruction;
using CODWER.RERU.Personal.DataTransferObjects.Instructions;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructionController : BaseController
    {
        [HttpPost]
        public async Task<int> AddInstruction([FromBody] AddEditInstructionDto dto)
        {
            var command = new AddInstructionCommand(dto);

            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task UpdateInstruction([FromBody] AddEditInstructionDto dto)
        {
            var command = new UpdateInstructionCommand(dto);

            await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task RemoveInstruction([FromRoute] int id)
        {
            var command = new RemoveInstructionCommand
            {
                Id = id
            };

            await Mediator.Send(command);
        }
    }
}