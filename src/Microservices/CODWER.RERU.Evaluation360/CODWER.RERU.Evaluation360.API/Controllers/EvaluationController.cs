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
using RERU.Data.Entities.Evaluation360;
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

        [HttpDelete("{id}/delete")]
        public async Task DeleteEvaluation([FromRoute] int id)
        {
            await Sender.Send(new DeleteEvaluationQuery(id));
        }

        [HttpPut("update")]
        public async Task UpdateEvaluation([FromBody] EditEvaluationDto dto)
        {
            var command = new UpdateEvaluationCommand(dto);
            await Sender.Send(command);
        }

        [HttpPut("confirm")]
        public async Task ConfirmEvaluation([FromBody] EditEvaluationDto dto)
        {
            var command = new ConfirmEvaluationCommand(dto);
            await Sender.Send(command);
        }

        [HttpPut("accept")]
        public async Task AcceptEvaluation([FromBody] AcceptRejectEvaluationDto dto)
        {
            var command = new AcceptEvaluationCommand(dto);
            await Sender.Send(command);
        }

        [HttpPut("reject")]
        public async Task RejectEvaluation([FromBody] AcceptRejectEvaluationDto dto)
        {
            var command = new RejectEvaluationCommand(dto);
            await Sender.Send(command);
        }

        [HttpPut("counter-sign-accept")]
        public async Task CounterSignAccept([FromBody] CounterSignAcceptRejectEvaluationDto dto)
        {
            var command = new CounterSignAcceptEvaluationCommand(dto);
            await Sender.Send(command);
        }

        
        [HttpPut("counter-sign-reject")]
        public async Task CounterSignReject([FromBody] CounterSignAcceptRejectEvaluationDto dto)
        {
            var command = new CounterSignRejectEvaluationCommand(dto);
            await Sender.Send(command);
        }

        [HttpPut("evaluated-know")]
        public async Task EvaluatedKnow([FromBody] EvaluatedKnowDto dto)
        {
            var command = new EvaluatedKnowCommand(dto);
            await Sender.Send(command);
        }
    }
}