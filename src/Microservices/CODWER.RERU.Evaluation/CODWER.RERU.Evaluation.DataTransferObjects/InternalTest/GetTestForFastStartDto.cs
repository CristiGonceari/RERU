using System;

namespace CODWER.RERU.Evaluation.DataTransferObjects.InternalTest
{
    public class GetTestForFastStartDto
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string TestTemplateName { get; set; }
        public DateTime ProgrammedTime { get; set; }
        public DateTime? EndProgrammedTime { get; set; }
        public bool CanStartWithoutConfirmation { get; set; }
    }
}
