using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.AddContractorVacation
{
    public class AddVacationCommandHandler : IRequestHandler<AddVacationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IVacationIntervalService _vacationIntervalService;
        private readonly IVacationTemplateParserService _vacationTemplateParserService;


        public AddVacationCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            IVacationIntervalService vacationIntervalService,
            IVacationTemplateParserService vacationTemplateParserService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _vacationIntervalService = vacationIntervalService;
            _vacationTemplateParserService = vacationTemplateParserService;
        }

        public async Task<int> Handle(AddVacationCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Vacation>(request.Data);
            item.ContractorId = request.Data.ContractorId;
            item.Status = StageStatusEnum.Approved;
            item.CountDays = await _vacationIntervalService.GetVacationDaysByInterval(request.Data.FromDate, request.Data.ToDate);
            item.VacationRequestId = await _vacationTemplateParserService.SaveRequestFile(item.ContractorId, item);
            item.VacationOrderId = await _vacationTemplateParserService.SaveOrderFile(item.ContractorId, item);

            await _appDbContext.Vacations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
