using MediatR;
using System;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.RemoveTimeSheetValues
{
    public class RemoveTimeSheetTableCommand : IRequest<Unit>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
