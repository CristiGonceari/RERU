using System;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetMyEvaluations;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : BaseController
    {
        [HttpGet("mine")]
        public async Task<PaginatedModel<EvaluationRowDto>> GetMyEvaliations([FromQuery] GetMyEvaluationsQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpGet("{id}/edit")]
        public async Task<EditEvaluationDto> GetEdit([FromRoute] int id)
        {
            return await Sender.Send(new GetEditEvaluationQuery(id));
        }

        [HttpPost]
        public async Task<int> CreateEvaluation([FromBody] CreateEvaluationCommand command)
        {
            return await Sender.Send(command);
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

        //  [HttpPut]
        // public async Task AcceptEvaluation([FromBody] EditEvaluationDto dto)
        // {
        //     var command = new UpdateEvaluationCommand(dto);
        //     await Sender.Send(command);
        // }

        //    [HttpPut]
        // public async Task RejectEvaluation([FromBody] EditEvaluationDto dto)
        // {
        //     var command = new UpdateEvaluationCommand(dto);
        //     await Sender.Send(command);
        // }

        //      [HttpPut]
        // public async Task CounterSignAccept([FromBody] EditEvaluationDto dto)
        // {
        //     var command = new UpdateEvaluationCommand(dto);
        //     await Sender.Send(command);
        // }

        
        //      [HttpPut]
        // public async Task CounterSignReject([FromBody] EditEvaluationDto dto)
        // {
        //     var command = new UpdateEvaluationCommand(dto);
        //     await Sender.Send(command);
        // }

    }
}