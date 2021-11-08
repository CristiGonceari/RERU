using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestType
{
    public class GetTestTypeQueryHandler : IRequestHandler<GetTestTypeQuery, TestTypeDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTypeQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<TestTypeDto> Handle(GetTestTypeQuery request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<TestTypeDto>(testType);
        }
    }
}
