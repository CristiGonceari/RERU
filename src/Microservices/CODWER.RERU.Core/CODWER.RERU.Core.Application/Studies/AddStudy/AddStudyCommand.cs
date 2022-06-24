using CODWER.RERU.Core.DataTransferObjects.Studies;
using MediatR;

namespace CODWER.RERU.Core.Application.Studies.AddStudy
{
    public class AddStudyCommand : IRequest<int>
    {
        public AddStudyCommand(StudyDto data)
        {
            Data = data;
        }

        public StudyDto Data { get; set; }
    }
}
