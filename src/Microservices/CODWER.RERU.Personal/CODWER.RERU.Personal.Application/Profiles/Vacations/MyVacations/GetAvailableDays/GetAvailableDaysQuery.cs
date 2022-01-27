using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetAvailableDays
{
    public class GetAvailableDaysQuery : IRequestHandler<GetAvailableDays, double>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IVacationIntervalService _vacationIntervalService;

        public GetAvailableDaysQuery(IUserProfileService userProfileService, IVacationIntervalService vacationIntervalService)
        {
            _userProfileService = userProfileService;
            _vacationIntervalService = vacationIntervalService;
        }

        public async Task<double> Handle(GetAvailableDays request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            return await _vacationIntervalService.GetCalculatedContractorAvailableDays(contractorId, request.VacantionTypeId);
        }
    }
}
