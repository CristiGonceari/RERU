using CODWER.RERU.Core.Application.ModernLanguageLevels.AddModernLanguageLevel;
using CODWER.RERU.Core.Application.ModernLanguageLevels.BulkAddEditModernLanguageLevels;
using CODWER.RERU.Core.Application.ModernLanguageLevels.GetUserProfileModernLanguageLevels;
using CODWER.RERU.Core.Application.ModernLanguageLevels.RemoveUserProfileModernLanguageLevel;
using CODWER.RERU.Core.Application.ModernLanguageLevels.UpdateUserProfileModernLanguageLevel;
using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModernLanguageLevelController : BaseController
    {
        public ModernLanguageLevelController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<ModernLanguageLevelDto>> GetUserProfileModernLanguageLevel([FromQuery] GetUserProfileModernLanguageLevelsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateModernLanguageLevel([FromBody] AddEditModernLanguageLevelDto dto)
        {
            var command = new AddModernLanguageLevelCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("bulk-import")]
        public async Task<Unit> AddEditModernLanguageLevels([FromBody] List<AddEditModernLanguageLevelDto> dtoList)
        {
            var command = new BulkAddEditModernLanguageLevelsCommand(dtoList);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateModernLanguageLevel([FromBody] AddEditModernLanguageLevelDto dto)
        {
            var command = new UpdateUserProfileModernLanguageLevelCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveModernLanguageLevel([FromRoute] int id)
        {
            var command = new RemoveUserProfileModernLanguageLevelCommand { ModernLanguageLevelId = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
