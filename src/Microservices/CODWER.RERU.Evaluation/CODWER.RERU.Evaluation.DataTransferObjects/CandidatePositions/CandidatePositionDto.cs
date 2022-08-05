using System;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions
{
    public class CandidatePositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string ResponsiblePerson { get; set; }
        public int ResponsiblePersonId { get; set; }
        public List<SelectItem> RequiredDocuments { get; set; }
        public List<SelectItem> Events { get; set; }
    }
}
