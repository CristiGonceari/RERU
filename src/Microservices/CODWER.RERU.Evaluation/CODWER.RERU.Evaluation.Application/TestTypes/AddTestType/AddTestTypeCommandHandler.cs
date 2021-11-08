using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddTestType
{
    public class AddTestTypeCommandHandler : IRequestHandler<AddTestTypeCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddTestTypeCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddTestTypeCommand request, CancellationToken cancellationToken)
        {
            var newTestType = _mapper.Map<TestType>(request.Data);

            _appDbContext.TestTypes.Add(newTestType);

            await _appDbContext.SaveChangesAsync();

            return newTestType.Id;
        }

    }
}
