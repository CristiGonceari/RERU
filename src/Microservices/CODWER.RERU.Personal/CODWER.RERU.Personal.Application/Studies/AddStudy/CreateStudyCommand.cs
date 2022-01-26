using CODWER.RERU.Personal.DataTransferObjects.Studies;
using MediatR;

namespace CODWER.RERU.Personal.Application.Studies.AddStudy
{
    public class CreateStudyCommand : IRequest<int>
    {
        public CreateStudyCommand(StudyDataDto data)
        {
            Data = data;
        }

        public StudyDataDto Data { get; set; }
    }
}
