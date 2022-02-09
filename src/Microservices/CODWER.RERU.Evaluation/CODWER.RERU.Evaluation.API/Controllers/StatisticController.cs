using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Statistics.GetCategoryQuestionsStatistic;
using CODWER.RERU.Evaluation.Application.Statistics.GetTestQuestionsStatistic;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;
using CODWER.RERU.Evaluation.Application.Statistics.PrintCategoryQuestionsStatistic;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CODWER.RERU.Evaluation.Application.Statistics.PrintTestQuestionsStatistic;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticController : BaseController
    {
        [HttpGet("questions-test-type")]
        public async Task<List<TestQuestionStatisticDto>> GetTestQuestionsStatistics([FromQuery] GetTestQuestionsStatisticQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("questions-category")]
        public async Task<List<TestQuestionStatisticDto>> GetCategoryQuestionsStatistics([FromQuery] GetCategoryQuestionsStatisticQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("print-category-question-statistic")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintCategoryQuestionsStatistic([FromBody] PrintCategoryQuestionsStatisticCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print-test-question-statistic")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintTestQuestionsStatistic([FromBody] PrintTestQuestionsStatisticCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
