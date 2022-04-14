using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.DocumentsTemplates.AddDocumentTemplate;
using CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentCategoryKeys;
using CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentTemplate;
using CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentTemplates;
using CODWER.RERU.Evaluation.Application.DocumentsTemplates.RemoveDocumentTemplate;
using CODWER.RERU.Evaluation.Application.DocumentsTemplates.UpdateDocumentTemplate;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTemplateController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<AddEditDocumentTemplateDto>> GetDocuments([FromQuery] GetDocumentTemplatesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }


        [HttpGet("keys")]
        public async Task<List<DocumentTemplateKeyDto>> GetKeys([FromQuery] FileTypeEnum fileType)
        {
            var query = new GetDocumentKeysQuery { fileType = fileType };

            return await Mediator.Send(query);
        }

        [HttpGet("{id:int}")]
        public async Task<AddEditDocumentTemplateDto> GetDocument([FromRoute] int id)
        {
            var query = new GetDocumentTemplateQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> AddDocument([FromBody] AddDocumentTemplateCommand templateCommand)
        {
            var result = await Mediator.Send(templateCommand);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateDocument([FromBody] UpdateDocumentTemplateCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task<Unit> RemoveDocument([FromRoute] int id)
        {
            var command = new RemoveDocumentTemplateCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
