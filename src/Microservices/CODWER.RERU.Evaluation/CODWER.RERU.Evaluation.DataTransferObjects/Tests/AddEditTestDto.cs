using System;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class AddEditTestDto
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public int? EvaluatorId { get; set; }
        public bool? ShowUserName { get; set; }
        public int TestTypeId { get; set; }
        public int? EventId { get; set; }
        public int? LocationId { get; set; }
        public TestStatusEnum TestStatus { get; set; }
        public DateTime ProgrammedTime { get; set; }
    }
}
