using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Employers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.AddRequest
{
    public class AddDismissalRequestCommandHandler : IRequestHandler<AddDismissalRequestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IDismissalTemplateParserService _dismissalTemplateParser;
        private readonly ITemplateConvertor _templateConvertor;
        private readonly IStorageFileService _storageFileService;
        private readonly EmployerData _employerData;
        private readonly string _fileName;

        public AddDismissalRequestCommandHandler(AppDbContext appDbContext,
            IUserProfileService userProfileService,
            IDismissalTemplateParserService dismissalTemplateParserService,
            ITemplateConvertor templateConvertor,
            IStorageFileService storageFileService,
            IOptions<EmployerData> options)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _dismissalTemplateParser = dismissalTemplateParserService;

            _templateConvertor = templateConvertor;
            _storageFileService = storageFileService;
            _employerData = options.Value;
            _fileName = "ContractorTemplates/Requests/Cerere Cu Privire La Demisionare.html";
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
