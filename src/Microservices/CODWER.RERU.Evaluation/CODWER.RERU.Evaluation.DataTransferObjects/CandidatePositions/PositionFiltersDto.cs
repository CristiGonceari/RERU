using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions
{
    public class PositionFiltersDto
    {
        public string Name { get; set; }
        public string ResponsiblePersonName { get; set; }
        public MedicalColumnEnum? MedicalColumn { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
    }
}
