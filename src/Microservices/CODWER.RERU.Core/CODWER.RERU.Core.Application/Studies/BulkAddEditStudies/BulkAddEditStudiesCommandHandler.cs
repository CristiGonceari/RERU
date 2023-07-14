using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Studies.BulkAddEditStudies
{
    public class BulkAddEditStudiesCommandHandler : IRequestHandler<BulkAddEditStudiesCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddEditStudiesCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddEditStudiesCommand request, CancellationToken cancellationToken)
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
