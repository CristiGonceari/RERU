using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.QuestionCategories.AddQuestionCategory;
using CODWER.RERU.Evaluation.Application.QuestionCategories.DeleteQuestionCategory;
using CODWER.RERU.Evaluation.Application.QuestionCategories.EditQuestionCategory;
using CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategories;
using CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategoriesNonPaginated;
using CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategory;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionCategoryController: BaseController
    {
        [HttpGet("{id}")]
        public async Task<QuestionCategoryDto> GetQuestionCategory([FromRoute] int id)
        {
            return await Mediator.Send(new GetQuestionCategoryQuery { Id = id });
        }

        [HttpGet("list")]
        public async Task<PaginatedModel<QuestionCategoryDto>> GetAllQuestionCategories([FromQuery] GetQuestionCategoriesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("list-non-paginated")]
        public async Task<List<QuestionCategoryDto>> GetQuestionCategoriesNonPaginated([FromQuery] GetQuestionCategoriesNonPaginatedQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("create")]
        public async Task<int> CreateQuestionCategory([FromBody] AddEditQuestionCategoryDto request)
        {
            return await Mediator.Send(new AddQuestionCategoryCommand { Data = request });
        }

        [HttpPost("edit")]
        public async Task<Unit> EditQuestionCategory([FromBody] AddEditQuestionCategoryDto request)
        {
            return await Mediator.Send(new EditQuestionCategoryCommand { Data = request });
        }

        [HttpDelete("delete/{id}")]
        public async Task<Unit> DeleteQuestionCategory([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteQuestionCategoryCommand { Id = id });
        }

    }
}
