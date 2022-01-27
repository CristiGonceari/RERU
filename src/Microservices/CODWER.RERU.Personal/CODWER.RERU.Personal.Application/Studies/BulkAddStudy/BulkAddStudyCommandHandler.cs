using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Studies;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Studies.BulkAddStudy
{
    public class BulkAddStudyCommandHandler : IRequestHandler<BulkAddStudyCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddStudyCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddStudyCommand request, CancellationToken cancellationToken)
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
