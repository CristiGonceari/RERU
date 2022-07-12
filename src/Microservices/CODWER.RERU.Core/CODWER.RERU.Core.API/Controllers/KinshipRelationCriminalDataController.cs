using CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.AddKinshipRelationCriminalData;
using CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.GetUserProfileKinshipRelationCriminalData;
using CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.UpdateKinshipRelationCriminalData;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinshipRelationCriminalDataController : BaseController
    {
        public KinshipRelationCriminalDataController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<KinshipRelationCriminalDataDto> GetKinshipRelationCriminalData([FromRoute] int id)
        {
            var query = new GetUserProfileKinshipRelationCriminalDataQuery { UserProfileId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> AddKinshipRelationCriminalData([FromBody] KinshipRelationCriminalDataDto data)
        {
            var command = new AddKinshipRelationCriminalDataCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateKinshipRelationCriminalData([FromBody] KinshipRelationCriminalDataDto data)
        {
            var command = new UpdateKinshipRelationCriminalDataCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
