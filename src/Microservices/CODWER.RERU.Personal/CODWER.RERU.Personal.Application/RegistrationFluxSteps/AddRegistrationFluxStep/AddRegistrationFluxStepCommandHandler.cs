using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps.AddRegistrationFluxStep
{
    public class AddRegistrationFluxStepCommandHandler : IRequestHandler<AddRegistrationFluxStepCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddRegistrationFluxStepCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddRegistrationFluxStepCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<RegistrationFluxStep>(request.Data);

            await _appDbContext.RegistrationFluxSteps.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
