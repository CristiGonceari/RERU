using CODWER.RERU.Core.Application.Studies.AddStudy;
using CODWER.RERU.Core.Application.Studies.GetUserProfileStudies;
using CODWER.RERU.Core.Application.Studies.RemoveStudy;
using CODWER.RERU.Core.Application.Studies.UpdateStudy;
using CODWER.RERU.Core.DataTransferObjects.Studies;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class StudyController : BaseController
    {
        public StudyController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<StudyDto>> GetStudys([FromQuery] GetUserProfileStudiesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateStudy([FromBody] StudyDto dto)
        {
            var command = new AddStudyCommand(dto)
                ;
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateStudy([FromBody] StudyDto dto)
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
