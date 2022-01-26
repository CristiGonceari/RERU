using System.Collections.Generic;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using MediatR;

namespace CODWER.RERU.Personal.Application.Studies.BulkAddStudy
{
    public class BulkAddStudyCommand : IRequest<Unit>
    {
        public BulkAddStudyCommand(List<StudyDataDto> list)
        {
            Data = list;
        }

        public List<StudyDataDto> Data { get; set; }
    }
}
