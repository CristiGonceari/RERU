using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class TestFiltersDto
    {
        public string TestTemplateName { get; set; }
        public string UserName { get; set; }
        public string EvaluatorName { get; set; }
        public string Email { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public TestResultStatusEnum? ResultStatus { get; set; }
        public string ResultStatusValue { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
        public TestTemplateModeEnum Mode { get; set; }
    }
}
