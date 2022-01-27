using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DismissalRequests.AddDismissalRequest
{
    public class AddDismissalRequestCommandHandler : IRequestHandler<AddDismissalRequestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDismissalTemplateParserService _dismissalTemplateParser;

        public AddDismissalRequestCommandHandler(AppDbContext appDbContext, IDismissalTemplateParserService dismissalTemplateParser)
        {
            _appDbContext = appDbContext;
            _dismissalTemplateParser = dismissalTemplateParser;
        }

        public async Task<int> Handle(AddDismissalRequestCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Positions)
                .FirstAsync(x => x.Id == request.Data.ContractorId);

            var contractorPosition = contractor.GetCurrentPositionOnData(DateTime.Now);
            contractorPosition.ToDate = request.Data.From;

            var requestToAdd = new Data.Entities.ContractorEvents.DismissalRequest
            {
                From = request.Data.From,
                Status = StageStatusEnum.Approved,
                ContractorId = request.Data.ContractorId,
                PositionId = contractorPosition.Id,
                RequestId = await _dismissalTemplateParser.SaveRequestFile(request.Data.ContractorId, request.Data.From),
                OrderId = await _dismissalTemplateParser.SaveOrderFile(request.Data.ContractorId, request.Data.From)
            };

            await _appDbContext.DismissalRequests.AddAsync(requestToAdd);
            await _appDbContext.SaveChangesAsync();

            return requestToAdd.Id;
        }
    }
}