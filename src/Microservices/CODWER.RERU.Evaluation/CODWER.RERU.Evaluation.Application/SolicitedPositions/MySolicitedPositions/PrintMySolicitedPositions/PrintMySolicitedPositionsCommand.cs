using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.PrintMySolicitedPositions
{
    public class PrintMySolicitedPositionsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int? PositionId { get; set; }
        public string UserName { get; set; }
        public SolicitedPositionStatusEnum? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? TillDate { get; set; }
    }
}
