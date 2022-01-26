using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.GetAvailableDays
{
    public class GetAvailableDaysQueryHandler : IRequestHandler<GetAvailableDaysQuery, double>
    {
        private readonly IVacationIntervalService _vacationIntervalService;

        public GetAvailableDaysQueryHandler(IVacationIntervalService vacationIntervalService)
        {
            _vacationIntervalService = vacationIntervalService;
        }

        public async Task<double> Handle(GetAvailableDaysQuery request, CancellationToken cancellationToken)
        {
            return await _vacationIntervalService.GetCalculatedContractorAvailableDays(request.ContractorId, request.VacantionTypeId);
        }
    }
}

