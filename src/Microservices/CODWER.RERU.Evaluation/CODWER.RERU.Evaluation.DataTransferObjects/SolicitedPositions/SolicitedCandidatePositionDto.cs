using System;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions
{
    public class SolicitedCandidatePositionDto
    {
        public int? Id { get; set; }
        public int? UserProfileId { get; set; }
        public string UserProfileName { get; set; }
        public string UserProfileIdnp { get; set; }
        public int CandidatePositionId { get; set; }
        public string CandidatePositionName { get; set; }
        public int AttachedFilesCount { get; set; }
        public int RequiredAttachedFilesCount { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<EventsWithTestTemplateDto> Events { get; set; }
        public DateTime SolicitedTime { get; set; }
        public SolicitedPositionStatusEnum? SolicitedTestStatus { get; set; }
    }
}
