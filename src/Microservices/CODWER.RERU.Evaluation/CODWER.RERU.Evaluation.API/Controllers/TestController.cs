using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Tests.AddMyPoll;
using CODWER.RERU.Evaluation.Application.Tests.AddTests;
using CODWER.RERU.Evaluation.Application.Tests.DeleteTest;
using CODWER.RERU.Evaluation.Application.Tests.EditTestStatus;
using CODWER.RERU.Evaluation.Application.Tests.ExportTests;
using CODWER.RERU.Evaluation.Application.Tests.FinalizeTest;
using CODWER.RERU.Evaluation.Application.Tests.GetMyPollsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsCountWithoutEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEventByDate;
using CODWER.RERU.Evaluation.Application.Tests.GetPollResult;
using CODWER.RERU.Evaluation.Application.Tests.GetTest;
using CODWER.RERU.Evaluation.Application.Tests.GetTests;
using CODWER.RERU.Evaluation.Application.Tests.PrintTests;
using CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserEvaluatedTests;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserPollsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserTests;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserTestsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserEvaluatedTests;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserPollsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserTests;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserTestsByEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests.CountMyEvaluatedTests;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests.GetMyEvaluatedTestsByDate;
using CODWER.RERU.Evaluation.Application.Tests.GetTestSettings;
using CODWER.RERU.Evaluation.Application.Tests.GetTestDocumentReplacedKeys;
using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly IGetTestDocumentReplacedKeys _getTestDocumentReplacedKeys;

        public TestController(IGetTestDocumentReplacedKeys getTestDocumentReplacedKeys)
        {
            _getTestDocumentReplacedKeys = getTestDocumentReplacedKeys;
        }

        [HttpGet("{id}")]
        public async Task<TestDto> GetTest([FromRoute] int id)
        {
            var query = new GetTestQuery {Id = id};
            return await Mediator.Send(query);
        }

        [HttpGet("setting/{id}")]
        public async Task<TestDto> GetTestSetting([FromRoute] int id)
        {
            var query = new GetTestSettingsQuery { Id = id };
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

        [HttpGet("my-evaluated-tests-count")]
        public async Task<List<TestCount>> GetMyEvaluatedTestsCount([FromQuery] CountMyEvaluatedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-evaluated")]
        public async Task<PaginatedModel<TestDto>> GetMyEvaluatedTests([FromQuery] GetMyEvaluatedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-tests-evaluated-by-date")]
        public async Task<PaginatedModel<TestDto>> GetMyEvaluatedTestsByDate([FromQuery] GetMyEvaluatedTestsByDateQuery query)
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

        [HttpGet("user-tests")]
        public async Task<PaginatedModel<TestDto>> GetUserTests([FromQuery] GetUserTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-tests-by-event")]
        public async Task<PaginatedModel<TestDto>> GetUserTestsByEvent([FromQuery] GetUserTestsByEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-polls-by-event")]
        public async Task<PaginatedModel<PollDto>> GetUserPollsByEvent([FromQuery] GetUserPollsByEventQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-evaluated-tests")]
        public async Task<PaginatedModel<TestDto>> GetUserEvaluatedTests([FromQuery] GetUserEvaluatedTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("export")]
        public async Task<FileContentResult> ExportTests()
        {
            byte[] answerBytes = await Mediator.Send(new ExportTestsQuery()) as byte[];

            var timeStamp = DateTime.Now;
            return File(answerBytes, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                $"AllTests_{timeStamp.Year}-{timeStamp.Month.ToString("00")}-{timeStamp.Day.ToString("00")}.xlsx");
        }

        [HttpPut("print-tests")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintTestsPdf([FromBody] PrintTestsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-user-tests")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserTestsPdf([FromBody] PrintUserTestsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-user-evaluated-tests")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserEvaluatedTestsPdf([FromBody] PrintUserEvaluatedTestsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-user-tests-by-event")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserTestsByEventPdf([FromBody] PrintUserTestsByEventCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-user-polls")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserPollsByEventPdf([FromBody] PrintUserPollsByEventCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("getTestRelacedKeys")]
        public async Task<string> GetTestDocumentReplacedKeys([FromQuery] GetTestDocumentReplacedKeysQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("getPDF")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetPDF([FromQuery] string source, string testName)
        {
            var result = await _getTestDocumentReplacedKeys.GetPdf(source, testName);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
