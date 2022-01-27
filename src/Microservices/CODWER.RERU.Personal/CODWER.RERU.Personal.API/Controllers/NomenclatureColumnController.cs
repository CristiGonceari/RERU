using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.BulkAddUpdateNomenclatureColumn;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.GetNomenclatureColumnsQuery;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureColumnController : BaseController
    {
        [HttpGet("{nomenclatureId}")]
        public async Task<NomenclatureTableHeaderDto> GetNomenclatureTableHeader([FromRoute] int nomenclatureId)
        {
            var query = new GetNomenclatureColumnsQuery {NomenclatureTypeId = nomenclatureId};

            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPut]
        public async Task<Unit> BulkAddUpdateNomenclatureTableHeader([FromBody] NomenclatureTableHeaderDto dto)
        {
            var query = new BulkAddUpdateNomenclatureColumnCommand() { Data = dto };

            var result = await Mediator.Send(query);

            return result;
        }
    }
}
