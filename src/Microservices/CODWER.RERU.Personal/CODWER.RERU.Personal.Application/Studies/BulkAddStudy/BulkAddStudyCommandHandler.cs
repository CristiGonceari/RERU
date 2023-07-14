using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using RERU.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var studies = await _appDbContext.Studies.Where(s => s.ContractorId == request.Data[0].ContractorId).ToListAsync();

            foreach (var studyDataDto in request.Data)
            {
                var existentStudy = studies.FirstOrDefault(s => s.Id == studyDataDto.Id);

                if (existentStudy == null)
                {

                    var item = _mapper.Map<Study>(studyDataDto);

                    await _appDbContext.Studies.AddAsync(item);
                }
                else
                {
                    _mapper.Map(studyDataDto, existentStudy);

                    studies.Remove(existentStudy);
                }
            }

            if (studies.Any())
            {
                _appDbContext.Studies.RemoveRange(studies);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
