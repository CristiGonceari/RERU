using CODWER.RERU.Core.DataTransferObjects.Studies;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.Studies.BulkAddEditStudies
{
    public class BulkAddEditStudiesCommand : IRequest<Unit>
    {
        public BulkAddEditStudiesCommand(List<StudyDto> list)
        {
            Data = list;
        }

        public List<StudyDto> Data { get; set; }
    }
}
