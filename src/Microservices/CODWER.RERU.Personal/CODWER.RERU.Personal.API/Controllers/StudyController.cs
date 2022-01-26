using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Studies.AddStudy;
using CODWER.RERU.Personal.Application.Studies.BulkAddStudy;
using CODWER.RERU.Personal.Application.Studies.GetContractorStudies;
using CODWER.RERU.Personal.Application.Studies.RemoveStudy;
using CODWER.RERU.Personal.Application.Studies.UpdateStudy;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<StudyDataDto>> GetStudys([FromQuery] GetContractorStudiesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateStudy([FromBody] StudyDataDto dto)
        {
            var command = new CreateStudyCommand(dto)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("bulk-import")]
        public async Task<Unit> CreateStudies([FromBody] List<StudyDataDto> dtoList)
        {
            var command = new BulkAddStudyCommand(dtoList);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateStudy([FromBody] StudyDataDto dto)
        {
            var command = new UpdateStudyCommand(dto);

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveStudy([FromRoute] int id)
        {
            var command = new RemoveStudyCommand { Id = id };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
