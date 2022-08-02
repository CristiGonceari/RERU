using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.UpdateRecommendationForStudy
{
    public class UpdateRecommendationForStudyCommandhandler : IRequestHandler<UpdateRecommendationForStudyCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateRecommendationForStudyCommandhandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateRecommendationForStudyCommand request, CancellationToken cancellationToken)
        {
            var toUpdate = await _appDbContext.RecommendationForStudies.FirstAsync(rfs => rfs.Id == request.Data.Id);

            _mapper.Map(request.Data, toUpdate);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
