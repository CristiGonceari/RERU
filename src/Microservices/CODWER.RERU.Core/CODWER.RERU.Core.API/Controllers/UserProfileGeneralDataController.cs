using CODWER.RERU.Core.Application.UserProfileGeneralDatas.AddUserProfileGeneralData;
using CODWER.RERU.Core.Application.UserProfileGeneralDatas.GetUserProfileGeneralData;
using CODWER.RERU.Core.Application.UserProfileGeneralDatas.UpdateUserProfileGeneralData;
using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileGeneralDataController : BaseController
    {
        public UserProfileGeneralDataController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<UserProfileGeneralDataDto> GetGeneralData([FromRoute] int id)
        {
            var query = new GetUserProfileGeneralDataQuery { UserProfileId = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> AddGeneralData([FromBody] UserProfileGeneralDataDto data)
        {
            var command = new AddUserProfileGeneralDataCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateGeneralData([FromBody] UserProfileGeneralDataDto data)
        {
            var command = new UpdateUserProfileGeneralDataCommand(data);

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
