using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using MediatR;

namespace CODWER.RERU.Personal.Application.VacationConfigurations.GetContractorAvailableVacations
{
    public class GetContractorAvailableVacationsQueryHandler : IRequestHandler<GetContractorAvailableVacationsQuery, double>
    {
        private readonly IVacationIntervalService _vacationIntervalService;
        private readonly IUserProfileService _userProfileService;

        public GetContractorAvailableVacationsQueryHandler(IVacationIntervalService vacationIntervalService, IUserProfileService userProfileService)
        {
            _vacationIntervalService = vacationIntervalService;
            _userProfileService = userProfileService;
        }

        public async Task<double> Handle(GetContractorAvailableVacationsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            Console.WriteLine("----GetContractorAvailable : ContractorId = " + contractorId);

            if (contractorId == null)
            {
                throw new Exception("Inexistent contractor");
            }

            return await _vacationIntervalService.GetContractorAvailableDays((int)contractorId);
        }
    }
}
