using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class UserDiagramDto
    {
        public UserDiagramDto()
        {
            TestsByTestTemplate = new List<TestsByTestTemplateDiagramDto>();
        }

        public int UserProfileId { get; set; }
        public string FullName { get; set; }
        public List<TestsByTestTemplateDiagramDto> TestsByTestTemplate { get; set; }
    }
}
