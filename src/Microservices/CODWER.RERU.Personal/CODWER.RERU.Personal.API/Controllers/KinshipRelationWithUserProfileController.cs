using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile;
using CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.BulkAddEditKinshipRelationWithUserProfiles;
using CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.GetKinshipRelationWithUserProfiles;
using CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.RemoveKinshipRelationWithUserProfile;
using CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.UpdateKinshipRelationWithUserProfile;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KinshipRelationWithUserProfileController : BaseController
    {
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

        [HttpPut("bulk-import")]
        public async Task<Unit> AddKinshipRelationWithUserProfile([FromBody] List<KinshipRelationWithUserProfileDto> dtoList)
        {
            var command = new BulkAddEditKinshipRelationWithUserProfilesCommand(dtoList);

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
