using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Tests.AddMyPoll;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.Application.Tests.AddTests;
using CODWER.RERU.Evaluation.Application.Tests.DeleteTest;
using CODWER.RERU.Evaluation.Application.Tests.EditTestStatus;
using CODWER.RERU.Evaluation.Application.Tests.ExportTests;
using CODWER.RERU.Evaluation.Application.Tests.FinalizeTest;
using CODWER.RERU.Evaluation.Application.Tests.GetMyPollsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetPollResult;
using CODWER.RERU.Evaluation.Application.Tests.GetTest;
using CODWER.RERU.Evaluation.Application.Tests.GetTests;
using CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEventByDate;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsCountWithoutEvent;

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
        public async Task<PaginatedModel<TestDto>> GetMyTestsWithoutEvent([FromQuery] GetMyTestsWithoutEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-without-event-by-date")]

        public async Task<PaginatedModel<TestDto>> GetMyTestsWithoutEventByDate([FromQuery] GetMyTestsWithoutEventByDateQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-without-event-count")]
        public async Task<List<TestCount>> GetMyTestsCountWithoutEvent([FromQuery] GetMyTestsCountWithoutEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-by-event")]
        public async Task<PaginatedModel<TestDto>> GetMyTestsByEvent([FromQuery] GetMyTestsByEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-polls-by-event")]
        public async Task<PaginatedModel<PollDto>> GetMyPollsByEvent([FromQuery] GetMyPollsByEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("tests")]
        public async Task<List<int>> AddTests([FromBody] AddTestsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("create-my-poll")]
        public async Task<int> CreateMyPoll([FromBody] AddMyPollCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("poll-result")]
        public async Task<PollResultDto> GetPollResult([FromQuery] GetPollResultQuery query)
        {
            return await Mediator.Send(query);
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

        [HttpGet("export")]
        public async Task<FileContentResult> ExportTests()
        {
            byte[] answerBytes = await Mediator.Send(new ExportTestsQuery()) as byte[];

            var timeStamp = DateTime.Now;
            return File(answerBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AllTests_{timeStamp.Year}-{timeStamp.Month.ToString("00")}-{timeStamp.Day.ToString("00")}.xlsx");
        }
    }
}
