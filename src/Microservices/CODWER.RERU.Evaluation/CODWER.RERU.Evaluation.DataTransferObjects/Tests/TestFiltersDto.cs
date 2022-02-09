using System;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class TestFiltersDto
    {
        public string TestTypeName { get; set; }
        public string UserName { get; set; }
        public TestStatusEnum? TestStatus { get; set; }
        public string LocationKeyword { get; set; }
        public string EventName { get; set; }
        public string Idnp { get; set; }
        public DateTime? ProgrammedTimeFrom { get; set; }
        public DateTime? ProgrammedTimeTo { get; set; }
    }
}
