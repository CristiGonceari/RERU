using CODWER.RERU.Evaluation.API.Config;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.EditSolicitedPositionStatus;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.DeleteMySolicitedPosition;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.EditMySolicitedPosition;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.PrintSolicitedPositions;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.UserSolicitedTests.GetUserSolicitedTests;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using GetMySolicitedPositionQuery = CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedPosition.GetMySolicitedPositionQuery;
using GetSolicitedPositionQuery = CODWER.RERU.Evaluation.Application.SolicitedPositions.GetSolicitedPosition.GetSolicitedPositionQuery;
using CODWER.RERU.Evaluation.Application.SolicitedPositions.GetAllSolicitedPositions;
using System.Collections.Generic;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitedTestController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> GetSolicitedTests([FromQuery] Application.SolicitedPositions.GetSolicitedPositions.GetSolicitedPositionQuery query)
        {
            return await Mediator.Send(query);

        }

        [HttpGet("all")]
        public async Task<List<SolicitedVacantPosition>> GetAllSolicitedTests([FromQuery] GetAllSolicitedPositionQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-solicited-test")]
        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> GetUserSolicitedTests([FromQuery] GetUserSolicitedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("by-id")]
        public async Task<SolicitedCandidatePositionDto> GetSolicitedTest([FromQuery] GetSolicitedPositionQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch("status")]
        public async Task<Unit> ChangeStatus([FromBody] EditSolicitedPositionStatusCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("my-solicited-tests")]
        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> GetMySolicitedTests([FromQuery] Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedTests.GetMySolicitedPositionQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-solicited-test/{id}")]
        public async Task<SolicitedCandidatePositionDto> GetMySolicitedTest([FromRoute] int id)
        {
            var query = new GetMySolicitedPositionQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpPost("add-my")]
        public async Task<AddSolicitedCandidatePositionResponseDto> AddTests([FromBody] AddMySolicitedPositionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("edit-my")]
        public async Task<AddSolicitedCandidatePositionResponseDto> UpdateEvent([FromBody] EditMySolicitedPositionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("delete-my/{id}")]
        public async Task<Unit> DeleteEvent([FromRoute] int id)
        {
            var command = new DeleteMySolicitedPositionCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintSolicitedTests([FromBody] PrintSolicitedPositionsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
