using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.GetCandidateContractorSteps
{
    public class GetCandidateContractorStepsQueryHandler :  IRequestHandler<GetCandidateContractorStepsQuery, CandidateContractorStepsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetCandidateContractorStepsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CandidateContractorStepsDto> Handle(GetCandidateContractorStepsQuery request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                                                    .Include(x => x.RegistrationFluxSteps)
                                                    .Include(x => x.UserProfile)
                                                    .ThenInclude(x => x.Role)
                                                    .FirstOrDefaultAsync(up => up.Id == request.ContractorId);

            var contractorDto = _mapper.Map<CandidateContractorStepsDto>(contractor);

            var registrationSteps = Enum.GetValues(typeof(RegistrationFluxStepEnum));
            var list = new List<int>();
            var isChecked = new List<CheckedRegistrationFluxStepsDto>();

            for (var i = 0; i < registrationSteps.Length; i++)
            {
                var check = contractor?.RegistrationFluxSteps.FirstOrDefault(rfs => rfs.Step.ToString() == registrationSteps.GetValue(i).ToString());

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
                else
                {
                    isChecked.Add(new CheckedRegistrationFluxStepsDto()
                    {
                        Value = (int)registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i],
                        Label = registrationSteps.Cast<RegistrationFluxStepEnum>().ToArray()[i].ToString(),
                        IsDone = true
                    });
                }
            };

            contractorDto.UnfinishedSteps = list;
            contractorDto.CheckedSteps = isChecked;

            return contractorDto;
        }
    }
}
