﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.RemoveContractor
{
    public class RemoveContractorCommandHandler : IRequestHandler<RemoveContractorCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveContractorCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveContractorCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors.FirstAsync(rt => rt.Id == request.Id);
            var userProfile = await _appDbContext.UserProfiles.FirstOrDefaultAsync(up => up.ContractorId == request.Id);

            _appDbContext.Contractors.Remove(contractor);

            if (userProfile !=  null)
            {
                userProfile.ContractorId = null;
            }

            RemoveAllData(request.Id);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private void RemoveAllData(int contractorId)
        {
            var contractorFiles = _appDbContext.ByteFiles.Where(x => x.ContractorId == contractorId);
            _appDbContext.ByteFiles.RemoveRange(contractorFiles);

            var contractorBulletins = _appDbContext.Bulletins.Where(x => x.ContractorId == contractorId);
            _appDbContext.Bulletins.RemoveRange(contractorBulletins);

            var contractorStudies = _appDbContext.Studies.Where(x => x.ContractorId == contractorId);
            _appDbContext.Studies.RemoveRange(contractorStudies);
        }
    }
}
