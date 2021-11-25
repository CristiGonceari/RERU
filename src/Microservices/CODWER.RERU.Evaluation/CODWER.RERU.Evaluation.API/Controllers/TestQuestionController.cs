using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;
using CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestion;
using CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestions;
using CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestionsSummary;
using CODWER.RERU.Evaluation.Application.TestQuestions.SaveTestQuestion;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestQuestionController : BaseController
    {
        [HttpGet]
        public async Task<TestQuestionDto> GetTestQuestion([FromQuery] GetTestQuestionQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("multiple-per-page")]
        public async Task<PaginatedModel<TestQuestionDto>> GetTestQuestions([FromQuery] GetTestQuestionsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("generate/{testId}")]
        public async Task<Unit> GetOptionsList([FromRoute] int testId)
        {
            return await Mediator.Send(new GenerateTestQuestionsCommand { TestId = testId });
        }

        [HttpGet("summary/{testId}")]
        public async Task<List<TestQuestionSummaryDto>> GetTestQuestionsSummary([FromRoute] int testId)
        {
            return await Mediator.Send(new GetTestQuestionsSummaryQuery { TestId = testId });
        }

        [HttpPost]
        public async Task<Unit> SaveTestQuestion([FromBody] SaveTestQuestionCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
