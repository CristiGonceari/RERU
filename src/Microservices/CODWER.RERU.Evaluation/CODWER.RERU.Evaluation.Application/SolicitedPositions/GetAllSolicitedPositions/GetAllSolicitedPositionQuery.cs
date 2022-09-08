using MediatR;
using RERU.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.GetAllSolicitedPositions
{
    public class GetAllSolicitedPositionQuery : IRequest<List<SolicitedVacantPosition>>
    {
    }
}
