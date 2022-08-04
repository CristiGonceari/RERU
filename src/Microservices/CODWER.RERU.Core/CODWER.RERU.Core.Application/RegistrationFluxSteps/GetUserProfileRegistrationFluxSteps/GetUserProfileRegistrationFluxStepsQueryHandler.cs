using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps.GetUserProfileRegistrationFluxSteps
{
    public class GetUserProfileRegistrationFluxStepsQueryHandler : IRequestHandler<GetUserProfileRegistrationFluxStepsQuery, List<RegistrationFluxStepDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetUserProfileRegistrationFluxStepsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<RegistrationFluxStepDto>> Handle(GetUserProfileRegistrationFluxStepsQuery request, CancellationToken cancellationToken)
        {
            var items =  await _appDbContext.RegistrationFluxSteps
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
