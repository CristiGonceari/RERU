﻿using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions
{
    public class AddEditSolicitedPositionDto
    {
        public int? Id { get; set; }
        public int? UserProfileId { get; set; }
        public int CandidatePositionId { get; set; }
        public SolicitedPositionStatusEnum? SolicitedTestStatus { get; set; }
    }
}
