using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Options.AddOption;
using CODWER.RERU.Evaluation.Application.Options.DeleteAllOptionsByQuestion;
using CODWER.RERU.Evaluation.Application.Options.DeleteOption;
using CODWER.RERU.Evaluation.Application.Options.EditOption;
using CODWER.RERU.Evaluation.Application.Options.GetOption;
using CODWER.RERU.Evaluation.Application.Options.GetOptions;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<OptionDto> GetOption([FromRoute] int id)
        {
            return await Mediator.Send(new GetOptionQuery { Id = id });
        }

        [HttpGet()]
        public async Task<List<OptionDto>> GetOptions([FromQuery] GetOptionsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddOption([FromBody] AddOptionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch]
        public async Task<Unit> EditOption([FromBody] EditOptionsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteOption([FromRoute] int id)
        {
            var command = new DeleteOptionCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpDelete("all/{questionId}")]
        public async Task<Unit> DeleteAllOptionsByQuestion([FromRoute] int questionId)
        {
            return await Mediator.Send(new DeleteAllOptionsByQuestionCommand { QuestionUnitId = questionId });
        }
    }
}
