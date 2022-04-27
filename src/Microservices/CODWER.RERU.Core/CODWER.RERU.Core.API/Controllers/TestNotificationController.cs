using CODWER.RERU.Core.Application.Internal.GetTestIdFastStart;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestNotificationController : BaseController
    {
        public TestNotificationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("test-notification")]
        public async Task<TestDataDto> GetTestId ()
        {
            return await Mediator.Send(new GetTestIdFastStartQuery());
        }
    }
}
