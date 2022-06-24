using CODWER.RERU.Core.Application.KinshipRelations.AddKinshipRelation;
using CODWER.RERU.Core.Application.KinshipRelations.GetUserProfileKinshipRelations;
using CODWER.RERU.Core.Application.KinshipRelations.RemoveKinshipRelation;
using CODWER.RERU.Core.Application.KinshipRelations.UpdateKinshipRelation;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinshipRelationController : BaseController
    {
        public KinshipRelationController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<KinshipRelationDto>> GetKinshipRelation([FromQuery] GetUserProfileKinshipRelationsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateKinshipRelation([FromBody] KinshipRelationDto dto)
        {
            var command = new AddKinshipRelationCommand(dto)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateKinshipRelation([FromBody] KinshipRelationDto dto)
        {
            var command = new UpdateKinshipRelationCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveKinshipRelation([FromRoute] int id)
        {
            var command = new RemoveKinshipRelationCommand { Id = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
