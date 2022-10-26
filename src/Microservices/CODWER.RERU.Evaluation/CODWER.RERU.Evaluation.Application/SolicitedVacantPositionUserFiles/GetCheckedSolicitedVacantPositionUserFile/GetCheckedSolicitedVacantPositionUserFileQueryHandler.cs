﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetCheckedSolicitedVacantPositionUserFile
{
    public class GetCheckedSolicitedVacantPositionUserFileQueryHandler : IRequestHandler<GetCheckedSolicitedVacantPositionUserFileQuery, bool>
    {
        private readonly AppDbContext _appDbContext;

        public GetCheckedSolicitedVacantPositionUserFileQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Handle(GetCheckedSolicitedVacantPositionUserFileQuery request, CancellationToken cancellationToken)
        {
            var solicitedVacantPositionUserFiles = await _appDbContext.SolicitedVacantPositions
                .Include(svp => svp.SolicitedVacantPositionUserFiles)
                .Include(svp => svp.CandidatePosition.RequiredDocumentPositions)
                .FirstOrDefaultAsync(svp => svp.Id == request.Id);

            return solicitedVacantPositionUserFiles.SolicitedVacantPositionUserFiles.Count() == solicitedVacantPositionUserFiles.CandidatePosition.RequiredDocumentPositions.Count();
        }
    }
}
