using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.AddNomenclatureRecord;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.DisableNomenclatureRecord;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.GetNomenclatureRecords;
using CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.UpdateNomenclatureRecord;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureRecordController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<NomenclatureRecordDto>> GetNomenclatureRecords([FromQuery] GetNomenclatureRecordsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPut("create")]
        public async Task<int> CreateNomenclatureRecord([FromBody] NomenclatureRecordDto dto)
        {
            var command = new AddNomenclatureRecordCommand{Data = dto};

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("update")]
        public async Task<Unit> UpdateNomenclatureRecord([FromBody] NomenclatureRecordDto dto)
        {
            var command = new UpdateNomenclatureRecordCommand { Data = dto };

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch("disable/{id}")]
        public async Task<Unit> DisableNomenclatureRecord([FromRoute] int id)
        {
            var command = new DisableNomenclatureRecordCommand { Id = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
