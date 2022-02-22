using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.AddRequest
{
    public class AddDismissalRequestCommandHandler : IRequestHandler<AddDismissalRequestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IDismissalTemplateParserService _dismissalTemplateParser;

        public AddDismissalRequestCommandHandler(AppDbContext appDbContext,
            IUserProfileService userProfileService,
            IDismissalTemplateParserService dismissalTemplateParserService
            )
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _dismissalTemplateParser = dismissalTemplateParserService;
        }

        public async Task<int> Handle(AddDismissalRequestCommand request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var contractorPosition = await _appDbContext.Positions
                .OrderByDescending(x => x.FromDate)
                .FirstAsync(x => x.ContractorId == contractorId);

            var requestToAdd = new DismissalRequest
            {
                From = request.From,
                Status = StageStatusEnum.New,
                ContractorId = contractorId,
                PositionId = contractorPosition.Id,
                RequestId = await _dismissalTemplateParser.SaveRequestFile(contractorId, request.From)
            };

            await _appDbContext.DismissalRequests.AddAsync(requestToAdd);
            await _appDbContext.SaveChangesAsync();

            return requestToAdd.Id;
        }
    }
}
