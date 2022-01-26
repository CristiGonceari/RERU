using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.DocumentGenerator.GetDocumentById;
using CODWER.RERU.Personal.Application.DocumentGenerator.GetFilteredByEnum;
using CODWER.RERU.Personal.DataTransferObjects.Documents;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsGeneratorController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<DocumentTemplateGeneratorDto>> GetDocumentsByType([FromQuery] GetFilteredByEnumQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<AddEditDocumentTemplateDto> GetDocumentById([FromRoute] int id)
        {
            var query = new GetDocumentByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }
    }
}
