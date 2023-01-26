using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.PrintEvaluations;

public class PrintEvaluationsCommand : TableParameter, IRequest<FileDataDto>
{
    public string EvaluatedName { set; get; }
    public string EvaluatorName { set; get; }
    public string CounterSignerName { set; get; }
    public EvaluationTypeEnum? Type { set; get; }
    public decimal? Points { set; get; } = 0.00M;
    public EvaluationStatusEnum? Status { set; get; }
}