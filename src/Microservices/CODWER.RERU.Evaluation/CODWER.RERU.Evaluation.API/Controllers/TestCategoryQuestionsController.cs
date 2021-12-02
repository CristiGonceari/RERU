using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestCategoryQuestions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestCategoryQuestions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
