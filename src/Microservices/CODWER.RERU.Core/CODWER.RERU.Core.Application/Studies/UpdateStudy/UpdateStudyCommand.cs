using CODWER.RERU.Core.DataTransferObjects.Studies;
using MediatR;

namespace CODWER.RERU.Core.Application.Studies.UpdateStudy
{
    public class UpdateStudyCommand : IRequest<Unit>
    {
        public UpdateStudyCommand(StudyDto dto)
        {
            Data = dto;
        }

        public StudyDto Data { get; set; }
    }
}
