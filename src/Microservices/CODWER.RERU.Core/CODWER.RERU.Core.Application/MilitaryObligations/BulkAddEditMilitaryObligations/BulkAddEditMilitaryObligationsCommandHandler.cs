using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MilitaryObligations.BulkAddEditMilitaryObligations
{
    public class BulkAddEditMilitaryObligationsCommandHandler : IRequestHandler<BulkAddEditMilitaryObligationsCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddEditMilitaryObligationsCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddEditMilitaryObligationsCommand request, CancellationToken cancellationToken)
        {
            var obligations = await _appDbContext.MilitaryObligations.ToListAsync();

            foreach (var relationDto in request.Data)
            {
                var existentRelation = obligations.FirstOrDefault(s => s.Id == relationDto.Id);

                if (existentRelation == null)
                {

                    var item = _mapper.Map<MilitaryObligation>(relationDto);

                    await _appDbContext.MilitaryObligations.AddAsync(item);
                }
                else
                {
                    _mapper.Map(relationDto, existentRelation);

                    obligations.Remove(existentRelation);
                }
            }

            if (obligations.Any())
            {
                _appDbContext.MilitaryObligations.RemoveRange(obligations);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
