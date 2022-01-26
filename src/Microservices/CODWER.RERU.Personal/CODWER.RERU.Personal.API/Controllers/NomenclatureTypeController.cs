using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.AddNomenclatures;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclature;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclatures;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.RemoveNomenclatures;
using CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.UpdateNomenclatures;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclatureTypeController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<NomenclatureTypeDto>> GetNomenclatureTypes([FromQuery] GetNomenclaturesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<NomenclatureTypeDto> GetNomenclatureType([FromRoute] int id)
        {
            var query = new GetNomenclatureQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateNomenclatureType([FromBody] AddEditNomenclatureTypeDto command)
        {
            var result = await Mediator.Send(new AddNomenclatureCommand(command));

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateNomenclatureType([FromBody] AddEditNomenclatureTypeDto command)
        {
            var result = await Mediator.Send(new UpdateNomenclatureCommand(command));

            return result;
        }

        [HttpPatch("{id}")]
        public async Task<Unit> RemoveNomenclatureType([FromRoute] int id)
        {
            var command = new DisableNomenclatureCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
