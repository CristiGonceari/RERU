using System;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.PrintAllTimeSheetTables
{
    public class PrintAllTimeSheetTableCommand : IRequest<ExportTimeSheetDto>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
