using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.SolicitedTests.EditSolicitedTestStatus;
using CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTest;
using CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTests;
using CODWER.RERU.Evaluation.Application.SolicitedTests.GetUserSolicitedTests;
using CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.AddMySolicitedTest;
using CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.DeleteMySolicitedTest;
using CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.EditMySolicitedTest;
using CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.GetMySolicitedTest;
using CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.GetMySolicitedTests;
using CODWER.RERU.Evaluation.Application.SolicitedTests.PrintSolicitetTests;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitedTestController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> GetSolicitedTests([FromQuery] GetSolicitedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-solicited-test")]
        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> GetUserSolicitedTests([FromQuery] GetUserSolicitedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("by-id")]
        public async Task<SolicitedCandidatePositionDto> GetSolicitedTest([FromQuery] GetSolicitedTestQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch("status")]
        public async Task<Unit> ChangeStatus([FromBody] EditSolicitedTestStatusCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("my-solicited-tests")]
        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> GetMySolicitedTests([FromQuery] GetMySolicitedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-solicited-test/{id}")]
        public async Task<SolicitedCandidatePositionDto> GetMySolicitedTest([FromRoute] int id)
        {
            var query = new GetMySolicitedTestQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpPost("add-my")]
        public async Task<AddSolicitedCandidatePositionResponseDto> AddTests([FromBody] AddMySolicitedTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("edit-my")]
        public async Task<AddSolicitedCandidatePositionResponseDto> UpdateEvent([FromBody] EditMySolicitedTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("delete-my/{id}")]
        public async Task<Unit> DeleteEvent([FromRoute] int id)
        {
            var command = new DeleteMySolicitedTestCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintSolicitedTests([FromBody] PrintSolicitedTestsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
