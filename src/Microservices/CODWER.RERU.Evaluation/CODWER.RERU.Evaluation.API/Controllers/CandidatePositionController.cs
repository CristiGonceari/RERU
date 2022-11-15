using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.CandidatePositions.AddCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.DeleteCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePositions;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionsSelectValues;
using CODWER.RERU.Evaluation.Application.CandidatePositions.PrintCandidatePosition;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionDiagram;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetUserSolicitedPositionDiagram;
using CODWER.RERU.Evaluation.Application.CandidatePositions.ChangeCandidatePositionStatus;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatePositionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<CandidatePositionDto> GetCandidatePosition([FromRoute] int id)
        {
            return await Mediator.Send(new GetCandidatePositionQuery { Id = id });
        }

        [HttpGet("diagram")]
        public async Task<PositionDiagramDto> GetPositionDiagram([FromQuery] GetPositionDiagramQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-diagram")]
        public async Task<UserPositionDiagramDto> GetUserPositionDiagram([FromQuery] GetUserSolicitedPositionDiagramQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<CandidatePositionDto>> GetCandidatePositions([FromQuery] GetCandidatePositionsQuery query)
        {
            return await Mediator.Send(query);
        }


        [HttpGet("select-values")]
        public async Task<List<SelectItem>> GetCandidatePositionValue([FromQuery] GetPositionsSelectValuesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddEditCandidatePosition([FromBody] AddCandidatePositionCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPatch]
        public async Task<Unit> EditCandidatePosition([FromBody] EditCandidatePositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch("change-status")]
        public async Task<Unit> EditCandidatePositionStatus([FromBody] ChangeCandidatePositionStatusCommand statusCommand)
        {
            var result = await Mediator.Send(statusCommand);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteCandidatePosition([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteCandidatePositionCommand { Id = id });
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintCandidatePosition([FromBody] PrintCandidatePositionCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
