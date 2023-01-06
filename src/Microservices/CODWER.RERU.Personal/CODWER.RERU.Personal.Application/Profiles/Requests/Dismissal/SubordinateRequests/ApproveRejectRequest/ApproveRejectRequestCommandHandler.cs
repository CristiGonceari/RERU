using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CVU.ERP.Common;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.ApproveRejectRequest
{
    public class ApproveRejectRequestCommandHandler : IRequestHandler<ApproveRejectRequestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDismissalTemplateParserService _dismissalTemplateParserService;
        private readonly IDateTime _dateTime;

        public ApproveRejectRequestCommandHandler(AppDbContext appDbContext, IDismissalTemplateParserService dismissalTemplateParserService, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dismissalTemplateParserService = dismissalTemplateParserService;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(ApproveRejectRequestCommand request, CancellationToken cancellationToken)
        {
            var dismissalRequest = await _appDbContext.DismissalRequests
                .Include(x=>x.Contractor)
                    .ThenInclude(x => x.Positions)
                        .ThenInclude(x=>x.Role)
                .FirstAsync(x=>x.Id == request.Data.Id);

            if (request.Data.Approve)
            {
                dismissalRequest.Status = StageStatusEnum.Approved;
                dismissalRequest.OrderId = await _dismissalTemplateParserService.SaveOrderFile(dismissalRequest.ContractorId, dismissalRequest.From);

                var position = dismissalRequest.Contractor.GetCurrentPositionOnData(_dateTime.Now);
                position.ToDate = dismissalRequest.From;
            }
            else
            {
                dismissalRequest.Status = StageStatusEnum.Rejected;
            }

            await _appDbContext.SaveChangesAsync();
            return Unit.Value;
        }
    }
}