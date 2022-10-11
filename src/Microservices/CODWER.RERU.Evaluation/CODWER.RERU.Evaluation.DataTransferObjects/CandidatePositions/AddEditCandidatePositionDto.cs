using RERU.Data.Entities.Enums;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions
{
    public class AddEditCandidatePositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public MedicalColumnEnum? MedicalColumn { get; set; }
        public List<AssignRequiredDocumentsDto> RequiredDocuments { get; set; }
        public List<int> EventIds { get; set; }
        public List<int> UserProfileIds { get; set; }
    }
}
