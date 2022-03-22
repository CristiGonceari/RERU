using System;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class AddMyTestDto
    {
        public int TestTemplatesId { get; set; }
        public int? EventId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? ProgrammedTime { get; set; }
    }
}
