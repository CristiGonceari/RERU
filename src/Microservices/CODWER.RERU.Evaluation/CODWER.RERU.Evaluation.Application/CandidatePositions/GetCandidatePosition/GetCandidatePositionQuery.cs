﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePosition
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_POZITII_CANDIDATULUI)]
    public class GetCandidatePositionQuery : IRequest<CandidatePositionDto>
    {
        public int Id { get; set; }
    }
}
