using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers.Internal
{
    [Route("internal/api/[controller]")]
    [ApiController]
    public class InternalGetTestIdController : BaseController
    {
        [HttpGet()]
        public async Task<TestDataDto> GetUserTestId()
        {
            return await Mediator.Send(new GetTestIdForFastStartQuery());
        }
    }
}