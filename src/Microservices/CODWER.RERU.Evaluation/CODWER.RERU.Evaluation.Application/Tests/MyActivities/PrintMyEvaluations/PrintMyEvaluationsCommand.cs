using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyEvaluations
{
    public class PrintMyEvaluationsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string EvaluationName { get; set; }
        public string EvaluatedName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
