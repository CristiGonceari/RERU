using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.DismissalRequests.AddDismissalRequest;
using CODWER.RERU.Personal.Application.DismissalRequests.GetDismissalByContractorId;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DismissalFluxController: BaseController
    {
        [HttpPost]
        public async Task<int> CreateDismissalRequestByHr([FromBody] AddDismissalRequestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedModel<MyDismissalRequestDto>> GetDismissalRequestsByContractorId([FromQuery] DismissalByContractorIdQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
