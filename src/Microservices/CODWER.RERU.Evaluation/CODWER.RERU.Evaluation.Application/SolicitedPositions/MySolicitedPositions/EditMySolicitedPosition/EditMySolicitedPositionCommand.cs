﻿using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.EditMySolicitedPosition
{
    public class EditMySolicitedPositionCommand : IRequest<AddSolicitedCandidatePositionResponseDto>
    {
        public AddEditSolicitedPositionDto Data { get; set; }
    }
}
