using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Internal.GetContractors;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers.Internal
{
    [Route("internal/api/[controller]")]
    [ApiController]
    public class InternalContractorController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ContractorSelectItem>> GetContractors([FromQuery] GetInternalContractorsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }
    }
}
