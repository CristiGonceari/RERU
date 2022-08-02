using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps.GetContractorRegistrationFluxSteps
{
    public class GetContractorRegistrationFluxStepsQueryHandler : IRequestHandler<GetContractorRegistrationFluxStepsQuery, List<RegistrationFluxStepDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorRegistrationFluxStepsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<RegistrationFluxStepDto>> Handle(GetContractorRegistrationFluxStepsQuery request, CancellationToken cancellationToken)
        {
            var items = await _appDbContext.RegistrationFluxSteps
                                                .Where(rfs => rfs.ContractorId == request.ContractorId)
                                                .ToListAsync();

            if (request.Step != null)
            {
                items = items.Where(i => i.Step == request.Step).ToList();
            }

            var mapper = _mapper.Map<List<RegistrationFluxStepDto>>(items);

            return mapper;
        }
    }
}
