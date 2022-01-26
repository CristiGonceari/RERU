using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacationDaysInterval
{
    public class GetVacationDaysIntervalQueryHandler : IRequestHandler<GetVacationDaysIntervalQuery, int>
    {
        private readonly IVacationIntervalService _vacationIntervalService;

        public GetVacationDaysIntervalQueryHandler(IVacationIntervalService vacationIntervalService)
        {
            _vacationIntervalService = vacationIntervalService;
        }

        public async Task<int> Handle(GetVacationDaysIntervalQuery request, CancellationToken cancellationToken)
        {
            return await _vacationIntervalService.GetVacationDaysByInterval(request.From, request.To);
        }
    }
}
