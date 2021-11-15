using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Statistics.GetCategoryQuestionsStatistic;
using CODWER.RERU.Evaluation.Application.Statistics.GetTestQuestionsStatistic;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;

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
    }
}
