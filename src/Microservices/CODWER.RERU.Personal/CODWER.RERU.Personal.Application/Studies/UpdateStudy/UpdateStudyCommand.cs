using CODWER.RERU.Personal.DataTransferObjects.Studies;
using MediatR;

namespace CODWER.RERU.Personal.Application.Studies.UpdateStudy
{
    public class UpdateStudyCommand : IRequest<Unit>
    {
        public UpdateStudyCommand(StudyDataDto dto)
        {
            Data = dto;
        }

        public StudyDataDto Data { get; set; }
    }
}
