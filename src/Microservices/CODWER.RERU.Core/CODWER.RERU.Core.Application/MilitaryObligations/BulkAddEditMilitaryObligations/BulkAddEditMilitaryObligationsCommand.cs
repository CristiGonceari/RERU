using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.MilitaryObligations.BulkAddEditMilitaryObligations
{
    public class BulkAddEditMilitaryObligationsCommand : IRequest<Unit>
    {
        public BulkAddEditMilitaryObligationsCommand(List<MilitaryObligationDto> list)
        {
            Data = list;
        }

        public List<MilitaryObligationDto> Data { get; set; }
    }
}
