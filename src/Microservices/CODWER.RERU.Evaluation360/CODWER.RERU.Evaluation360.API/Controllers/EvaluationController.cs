using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Accept;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignAccept;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignReject;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEvaluationRowDto;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Reject;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.EvaluatedKnow;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.DeleteEvaluation;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : BaseController
    {
        [HttpGet("mine")]
        public async Task<PaginatedModel<EvaluationRowDto>> EvaluationRowDto([FromQuery] EvaluationRowDtoQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<GetEvaluationDto> GetEdit([FromRoute] int id)
        {
            return await Sender.Send(new GetEditEvaluationQuery(id));
        }

        [HttpPost]
        public async Task CreateEvaluation([FromBody] CreateEvaluationsCommand command)
        {
            await Sender.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task DeleteEvaluation([FromRoute] int id)
        {
            await Sender.Send(new DeleteEvaluationQuery(id));
        }

        [HttpPut("{id}/update")]
        public async Task UpdateEvaluation([FromRoute] int id, [FromBody] EditEvaluationDto dto)
        {
            var command = new UpdateEvaluationCommand(id, dto);
            await Sender.Send(command);
        }

        [HttpPut("{id}/confirm")]
        public async Task ConfirmEvaluation([FromRoute] int id, [FromBody] EditEvaluationDto dto)
        {
            var command = new ConfirmEvaluationCommand(id, dto);
            await Sender.Send(command);
        }

        [HttpPut("{id}/accept")]
        public async Task AcceptEvaluation([FromRoute] int id, [FromBody] AcceptRejectEvaluationDto dto)
        {
            var command = new AcceptEvaluationCommand(id, dto);
            await Sender.Send(command);
        }

        [HttpPut("{id}/reject")]
        public async Task RejectEvaluation([FromRoute] int id, [FromBody] AcceptRejectEvaluationDto dto)
        {
            var command = new RejectEvaluationCommand(id, dto);
            await Sender.Send(command);
        }

        [HttpPut("{id}/counter-sign-accept")]
        public async Task CounterSignAccept([FromRoute] int id, [FromBody] CounterSignAcceptRejectEvaluationDto dto)
        {
            var command = new CounterSignAcceptEvaluationCommand(id, dto);
            await Sender.Send(command);
        }

        
        [HttpPut("{id}/counter-sign-reject")]
        public async Task CounterSignReject([FromRoute] int id, [FromBody] CounterSignAcceptRejectEvaluationDto dto)
        {
            var command = new CounterSignRejectEvaluationCommand(id, dto);
            await Sender.Send(command);
        }

        [HttpPut("{id}/evaluated-know")]
        public async Task EvaluatedKnow([FromRoute] int id)
        {
            var command = new EvaluatedKnowCommand(id);
            await Sender.Send(command);
        }
    }
}