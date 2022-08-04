using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePositions;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatePositionController : BaseController
    {
        public CandidatePositionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<PaginatedModel<CandidatePositionDto>> GetCandidatePositions([FromQuery] GetCandidatePositionsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
