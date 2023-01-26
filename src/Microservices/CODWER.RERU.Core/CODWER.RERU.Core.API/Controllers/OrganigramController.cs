using CODWER.RERU.Core.Application.Organigrams.GetOrganigramContent;
using CODWER.RERU.Core.Application.Organigrams.GetOrganigramUsers;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.OrganigramService.Models;
using CVU.ERP.OrganigramService.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganigramController : BaseController
    {
        private readonly IGetOrganigramService _getOrganigramService;

        public OrganigramController(IGetOrganigramService getOrganigramService, IMediator mediator) : base(mediator)
        {
            _getOrganigramService = getOrganigramService;
        }

        [HttpGet("organigram-head")]
        public async Task<OrganigramHead> GetOrganigramHead()
        {
            var result = await _getOrganigramService.GetOrganigramHead();

            return result;
        }

        [HttpGet("organigram-content")]
        public async Task<List<OrganigramContent>> GetOrganigramContent([FromQuery] GetOrganigramContentQuery query)
        {
            return await Mediator.Send(query);

        }

        [HttpGet("organigram-users")]
        public async Task<List<UserProfileDto>> GetOrganigramUsers([FromQuery] GetOrganigramUsersQuery query)
        {
            return await Mediator.Send(query);

        }
    }
}
