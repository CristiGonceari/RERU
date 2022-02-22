using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions;

namespace CODWER.RERU.Evaluation.API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class TestCategoryQuestionsController : BaseController
    {
        [HttpGet]
        public async Task<TestCategoryQuestionContentDto> GetTestCategoriesQuestions([FromQuery] TestCategoryQuestionsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
