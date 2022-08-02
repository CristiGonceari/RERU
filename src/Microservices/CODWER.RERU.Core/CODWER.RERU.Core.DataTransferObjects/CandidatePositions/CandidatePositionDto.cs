using System;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace CODWER.RERU.Core.DataTransferObjects.CandidatePositions
{
    public class CandidatePositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<SelectItem> RequiredDocuments { get; set; }
        public List<SelectItem> Events { get; set; }
    }
}
