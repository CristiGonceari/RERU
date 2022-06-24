using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Studies.BulkAddStudies
{
    public class BulkAddStudiesCommandHandler : IRequestHandler<BulkAddStudiesCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddStudiesCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddStudiesCommand request, CancellationToken cancellationToken)
        {
            foreach (var studyDataDto in request.Data)
            {
                var item = _mapper.Map<Study>(studyDataDto);

                await _appDbContext.Studies.AddAsync(item);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
