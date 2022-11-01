using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices;
using CODWER.RERU.Evaluation.Application.Tests.AddMyPoll;
using CODWER.RERU.Evaluation.Application.Tests.AddTests;
using CODWER.RERU.Evaluation.Application.Tests.AddTests.SendEmailNotification;
using CODWER.RERU.Evaluation.Application.Tests.DeleteTest;
using CODWER.RERU.Evaluation.Application.Tests.EditTestStatus;
using CODWER.RERU.Evaluation.Application.Tests.FinalizeTest;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests.CountMyEvaluatedTests;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests.GetMyEvaluatedTestsByDate;
using CODWER.RERU.Evaluation.Application.Tests.GetMyPollsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsByEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsCountWithoutEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEventByDate;
using CODWER.RERU.Evaluation.Application.Tests.GetPollResult;
using CODWER.RERU.Evaluation.Application.Tests.GetTest;
using CODWER.RERU.Evaluation.Application.Tests.GetTestDocumentReplacedKeys;
using CODWER.RERU.Evaluation.Application.Tests.GetTests;
using CODWER.RERU.Evaluation.Application.Tests.GetTestSettings;
using CODWER.RERU.Evaluation.Application.Tests.PrintTests;
using CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest;
using CODWER.RERU.Evaluation.Application.Tests.StartTest;
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
using CODWER.RERU.Evaluation.Application.Tests.AddEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.FinalizeEvaluation;
using CODWER.RERU.Evaluation.Application.Tests.GetEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyPolls;
using CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyPollsCount;
using CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyTests;
using CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyTestsCount;
using CODWER.RERU.Evaluation.Application.Tests.PrintEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.SetTestResult;
using CODWER.RERU.Evaluation.Application.Tests.StartEvaluation;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserReceivedEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserEvaluations;
using CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserReceivedEvaluations;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.ImportProcesses;
using CVU.ERP.Module.Application.ImportProcesses.GetImportProcess;
using CVU.ERP.Module.Application.ImportProcesses.GetImportResult;
using CVU.ERP.Module.Application.ImportProcesses.StartImportProcess;

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

        [HttpGet("get-evaluations")]
        public async Task<PaginatedModel<TestDto>> GetEvaluations([FromQuery] GetEvaluationsQuery query)
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

        [HttpGet("my-activities/my-tests")]
        public async Task<PaginatedModel<TestDto>> GetMyTests([FromQuery] GetMyTestsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-activities/my-tests-count")]
        public async Task<List<TestCount>> GetMyTestsCount([FromQuery] GetMyTestsCountQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-activities/my-polls")]
        public async Task<PaginatedModel<PollDto>> GetMyPolls([FromQuery] GetMyPollsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("my-activities/my-polls-count")]
        public async Task<List<EventCount>> GetMyPollsCount([FromQuery] GetMyPollsCountQuery query)
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

        [HttpGet("my-evaluations")]
        public async Task<PaginatedModel<TestDto>> GetMyEvaluations([FromQuery] GetMyEvaluationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("tests")]
        public async Task<List<int>> AddTests([FromBody] AddTestsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("evaluations")]
        public async Task<List<int>> AddEvaluations([FromBody] AddEvaluationsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("process")]
        public async Task<int> StartAddProcess([FromBody] StartImportProcessCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("process/{id}")]
        public async Task<ProcessDataDto> GetImportProcess([FromRoute] int id)
        {
            var query = new GetImportProcessQuery() { ProcessId = id };

            return await Mediator.Send(query);
        }

        [HttpGet("process-result/{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile([FromRoute] string fileId)
        {
            var query = new GetImportResultQuery {FileId = fileId};

            var result = await Mediator.Send(query);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
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

        [HttpPatch("send-notification")]
        public async Task<Unit> SendEmailNotification([FromBody] SendEmailNotificationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("finalize")]
        public async Task<Unit> FinalizeTest([FromBody] FinalizeTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("finalize-evaluation")]
        public async Task<Unit> FinalizeEvaluation([FromBody] FinalizeEvaluationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("edit-status")]
        public async Task<Unit> EditTestStatus([FromBody] EditTestStatusCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("edit-result")]
        public async Task<Unit> SetTestResult([FromBody] SetTestResultCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("start-test")]
        public async Task<Unit> StartTest([FromBody] StartTestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("start-evaluation")]
        public async Task<Unit> StartEvaluation([FromBody] StartEvaluationCommand command)
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

        [HttpGet("user-evaluations")]
        public async Task<PaginatedModel<TestDto>> GetUserEvaluations([FromQuery] GetUserEvaluationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("user-received-evaluations")]
        public async Task<PaginatedModel<TestDto>> GetUserPersonalEvaluations([FromQuery] GetUserReceivedEvaluationsQuery query)
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

        [HttpPut("print-tests")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintTestsPdf([FromBody] PrintTestsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-evaluations")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEvaluationsPdf([FromBody] PrintEvaluationsCommand command)
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

        [HttpPut("print-user-evaluations")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserEvaluationsPdf([FromBody] PrintUserEvaluationsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-user-received-evaluations")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserPersonalEvaluationsPdf([FromBody] PrintUserReceivedEvaluationsCommand command)
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
