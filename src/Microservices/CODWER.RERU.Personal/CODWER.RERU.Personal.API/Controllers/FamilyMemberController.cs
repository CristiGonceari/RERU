using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.FamilyMembers.AddFamilyMember;
using CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMember;
using CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMembers;
using CODWER.RERU.Personal.Application.FamilyMembers.RemoveFamilyMember;
using CODWER.RERU.Personal.Application.FamilyMembers.UpdateFamilyMember;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMemberController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<FamilyMemberDto>> GetFamilyMembers([FromQuery] GetFamilyMembersQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<FamilyMemberDto> GetFamilyMember([FromRoute] int id)
        {
            var query = new GetFamilyMemberQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateFamilyMember([FromBody] AddFamilyMemberCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateFamilyMember([FromBody] UpdateFamilyMemberCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveFamilyMember([FromRoute] int id)
        {
            var command = new RemoveFamilyMemberCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}