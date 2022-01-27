using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Module.Application.Clients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.UserProfiles.ResetContractorPassword
{
    public class ResetContractorPasswordCommandHandler : IRequestHandler<ResetContractorPasswordCommand, Unit>
    {
        private readonly ICoreClient _coreClient;
        private readonly AppDbContext _appDbContext;

        public ResetContractorPasswordCommandHandler(ICoreClient coreClient, AppDbContext appDbContext)
        {
            _coreClient = coreClient;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(ResetContractorPasswordCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _appDbContext.UserProfiles.FirstAsync(up => up.ContractorId == request.ContractorId);

            try
            {
                await _coreClient.ResetPassword(userProfile.UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Unit.Value;
        }
    }
}
