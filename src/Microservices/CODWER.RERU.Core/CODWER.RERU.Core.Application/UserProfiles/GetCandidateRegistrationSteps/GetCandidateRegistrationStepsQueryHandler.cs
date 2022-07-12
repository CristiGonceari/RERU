using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.EnumConverters;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateRegistrationSteps
{
    public class GetCandidateRegistrationStepsQueryHandler : BaseHandler, IRequestHandler<GetCandidateRegistrationStepsQuery, CandidateRegistrationStepsDto>
    {

        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public GetCandidateRegistrationStepsQueryHandler(
            ICommonServiceProvider commonServiceProvider,
            ICurrentApplicationUserProvider currentApplicationUserProvider) : base(commonServiceProvider)
        {
            _currentUserProvider = currentApplicationUserProvider;
        }

        public async Task<CandidateRegistrationStepsDto> Handle(GetCandidateRegistrationStepsQuery request, CancellationToken cancellationToken)
        {
            var currentApplicationUser = await _currentUserProvider.Get();

            var userProfile = await AppDbContext.UserProfiles
                .Include(x => x.Department)
                .Include(x => x.RegistrationFluxSteps)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(up => up.Id == int.Parse(currentApplicationUser.Id));

            var userProfDto = Mapper.Map<CandidateRegistrationStepsDto>(userProfile);


            var registrationSteps = Enum.GetValues(typeof(RegistrationFluxStepEnum));
            var list = new List<int>();
            var isChecked = new List<CheckedRegistrationFluxStepsDto>();

            for (var i = 0; i < registrationSteps.Length; i++) 
            {
                var check = userProfile.RegistrationFluxSteps.FirstOrDefault(rfs => rfs.Step.ToString() == registrationSteps.GetValue(i).ToString());

                if (check == null)
                {
                    list.Add((int)registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i]);
                    isChecked.Add(new CheckedRegistrationFluxStepsDto()
                    {
                        Value = (int)registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i],
                        Label = registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i].ToString(),
                        IsDone = false
                    });
                }
                else {
                    isChecked.Add(new CheckedRegistrationFluxStepsDto()
                    {
                        Value = (int)registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i],
                        Label = registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i].ToString(),
                        IsDone = true
                    });
                }
            };

            userProfDto.UnfinishedSteps = list;
            userProfDto.CheckedSteps = isChecked;

            return userProfDto;
        }
    }
}
