using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.AddContractor
{
    public class AddContractorCommandHandler : IRequestHandler<AddContractorCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IContractorCodeGeneratorService _codeGeneratorService;

        public AddContractorCommandHandler(AppDbContext appDbContext, IMapper mapper, IContractorCodeGeneratorService codeGeneratorService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _codeGeneratorService = codeGeneratorService;
        }

        public async Task<int> Handle(AddContractorCommand request, CancellationToken cancellationToken)
        {
            var newContractor = _mapper.Map<Contractor>(request.Data);
            newContractor.Code = await _codeGeneratorService.Next();

            await _appDbContext.Contractors.AddAsync(newContractor);
            await _appDbContext.SaveChangesAsync();

            return newContractor.Id;
        }
    }
}
