using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.ModernLanguageLevels.AddModernLanguageLevel;
using CODWER.RERU.Personal.Application.ModernLanguageLevels.BulkAddEditModernLanguageLevels;
using CODWER.RERU.Personal.Application.ModernLanguageLevels.GetContractorModernLanguageLevels;
using CODWER.RERU.Personal.Application.ModernLanguageLevels.RemoveContractorModernLanguageLevel;
using CODWER.RERU.Personal.Application.ModernLanguageLevels.UpdateContractorModernLanguageLevel;
using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModernLanguageLevelController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ModernLanguageLevelDto>> GetUserProfileModernLanguageLevel([FromQuery] GetContractorModernLanguageLevelsQuery query)
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
            var command = new UpdateContractorModernLanguageLevelCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveModernLanguageLevel([FromRoute] int id)
        {
            var command = new RemoveContractorModernLanguageLevelCommand { ModernLanguageLevelId = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
