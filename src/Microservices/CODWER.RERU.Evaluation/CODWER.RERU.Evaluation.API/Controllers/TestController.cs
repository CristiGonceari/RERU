using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.Application.Tests.DeleteTest;
using CODWER.RERU.Evaluation.Application.Tests.EditTestStatus;
using CODWER.RERU.Evaluation.Application.Tests.FinalizeTest;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetTest;
using CODWER.RERU.Evaluation.Application.Tests.GetTests;
using CODWER.RERU.Evaluation.Application.Tests.GetTestsWithoutEvent;
using CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<TestDto> GetTest([FromRoute] int id)
        {
            var query = new GetTestQuery {Id = id};
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<TestDto>> GetTests([FromQuery] GetTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-without-event")]
        public async Task<PaginatedModel<TestDto>> GetMyTestsWithoutEvent([FromQuery] GetTestsWithoutEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-by-event")]
        public async Task<PaginatedModel<TestDto>> GetMyTestsByEvent([FromQuery] GetMyTestsByEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddTest([FromBody] AddTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("allow")]
        public async Task<Unit> SetConfirmationToStartTest([FromBody] SetConfirmationToStartTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("finalize")]
        public async Task<Unit> FinalizeTest([FromBody] FinalizeTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("edit-status")]
        public async Task<Unit> EditTestStatus([FromBody] EditTestStatusCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Unit> DeleteTest([FromQuery] DeleteTestCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
