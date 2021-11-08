using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.AddOption
{
    public class AddOptionCommandHandler : IRequestHandler<AddOptionCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddOptionCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddOptionCommand request, CancellationToken cancellationToken)
        {
            var newOption = _mapper.Map<Option>(request.Input);

            await _appDbContext.Options.AddAsync(newOption);

            await _appDbContext.SaveChangesAsync();

            return newOption.Id;
        }
    }
}
