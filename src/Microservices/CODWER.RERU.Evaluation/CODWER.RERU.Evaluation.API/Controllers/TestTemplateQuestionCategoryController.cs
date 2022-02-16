using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;
using CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.DeleteQuestionCategoryFromTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.PreviewQuestionUnitsByTestTemplateCategory;
using CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.SetCategoriesSequence;
using CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.EditCategoriesSequenceTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.AssignQuestionCategoryToTestTemplate;
using CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.GetTestTemplateCategories;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTemplateQuestionCategoryController : BaseController
    {
        [HttpGet]
        public async Task<List<TestTypeQuestionCategoryDto>> GetTestTypeCategories([FromQuery] GetTestTemplateCategoriesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AssignQuestionCategoryToTestType([FromBody] AssignQuestionCategoryToTestTemplateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("sequence-type")]
        public async Task<Unit> EditCategoriesSequenceType([FromBody] EditCategoriesSequenceTemplateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("set-sequence")]
        public async Task<Unit> SetCategoriesSequence([FromBody] SetCategoriesSequenceCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("preview")]
        public async Task<List<CategoryQuestionUnitDto>> PreviewQuestionUnitsByTestTypeCategory([FromBody] PreviewQuestionUnitsByTestTemplateCategoryQuery command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<Unit> DeleteQuestionCategoryFromTestTypeId([FromQuery] DeleteQuestionCategoryFromTestTemplateCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}