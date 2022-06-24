using CODWER.RERU.Core.DataTransferObjects.Studies;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.Studies.BulkAddStudies
{
    public class BulkAddStudiesCommand : IRequest<Unit>
    {
        public BulkAddStudiesCommand(List<StudyDto> list)
        {
            Data = list;
        }

        public List<StudyDto> Data { get; set; }
    }
}
