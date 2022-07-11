using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions
{
    public class EventVacantPositionDto
    {
        public List<EventsWithTestTemplateDto> Events { get; set; }
        public List<RequiredDocumentDto> RequiredDocuments { get; set; }
    }
}
