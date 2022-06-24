using CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile;
using CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.GetKinshipRelationWithUserProfiles;
using CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.RemoveKinshipRelationWithUserProfile;
using CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinshipRelationWithUserProfileController : BaseController
    {
        public KinshipRelationWithUserProfileController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<KinshipRelationWithUserProfileDto>> GetKinshipRelationWithUserProfile([FromQuery] GetKinshipRelationWithUserProfilesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateKinshipRelationWithUserProfil([FromBody] KinshipRelationWithUserProfileDto dto)
        {
            var command = new AddKinshipRelationWithUserProfileCommand(dto)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateKinshipRelationWithUserProfil([FromBody] KinshipRelationWithUserProfileDto dto)
        {
            var command = new UpdateKinshipRelationWithUserProfileCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveKinshipRelationWithUserProfil([FromRoute] int id)
        {
            var command = new RemoveKinshipRelationWithUserProfileCommand { Id = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
