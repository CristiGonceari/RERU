using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.References.GetQuestionCategoryValue;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : BaseController
    {
        [HttpGet("question-types-value/select-values")]
        public async Task<List<SelectItem>> GetQuestionTypes()
        {
            var items = EnumConverter<QuestionTypeEnum>.SelectValues;

            return items;
        }

        [HttpGet("question-categories-value/select-values")]
        public async Task<List<SelectItem>> GetQuestionCategories()
        {
            var query = new GetQuestionCategoryValueQuery();

            return await Mediator.Send(query);
        }
    }
}
