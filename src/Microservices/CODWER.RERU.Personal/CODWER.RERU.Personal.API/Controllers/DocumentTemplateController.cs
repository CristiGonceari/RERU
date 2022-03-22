using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.DocumentTemplates.AddDocumentTemplate;
using CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplate;
using CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplates;
using CODWER.RERU.Personal.Application.DocumentTemplates.GetDoucmentKeys;
using CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate;
using CODWER.RERU.Personal.Application.DocumentTemplates.UpdateDocumentTemplate;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
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
        public async Task<List<DocumentTemplateCategoryDto>> GetKeys()
        {
            var query = new GetDocumentKeysQuery();

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
