using System;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintEvaluations
{
    public class PrintEvaluationsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string TestTemplateName { get; set; }
        public string UserName { get; set; }
        public string EvaluatorName { get; set; }
        public string Email { get; set; }
        public TestResultStatusEnum? ResultStatus { get; set; }
        public string ResultStatusValue { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public string EvaluatorIdnp { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
    }
}
