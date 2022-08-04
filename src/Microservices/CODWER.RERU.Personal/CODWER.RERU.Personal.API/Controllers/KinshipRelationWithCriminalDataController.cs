using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.AddKinshipRelationWithCriminalData;
using CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.GetContractorKinshipRelationWithCriminalData;
using CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.UpdateKinshipRelationWithCriminalData;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinshipRelationWithCriminalDataController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<KinshipRelationWithCriminalDataDto> GetKinshipRelationCriminalData([FromRoute] int id)
        {
            var query = new GetContractorKinshipRelationWithCriminalDataQuery { ContractorId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> AddKinshipRelationCriminalData([FromBody] KinshipRelationWithCriminalDataDto data)
        {
            var command = new AddKinshipRelationWithCriminalDataCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateKinshipRelationCriminalData([FromBody] KinshipRelationWithCriminalDataDto data)
        {
            var command = new UpdateKinshipRelationWithCriminalDataCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
