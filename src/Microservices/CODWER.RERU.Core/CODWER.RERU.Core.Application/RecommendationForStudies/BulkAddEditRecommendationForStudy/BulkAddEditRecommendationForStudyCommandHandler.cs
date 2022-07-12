using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.BulkAddEditRecommendationForStudy
{
    public class BulkAddEditRecommendationForStudyCommandHandler : IRequestHandler<BulkAddEditRecommendationForStudyCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public BulkAddEditRecommendationForStudyCommandHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(BulkAddEditRecommendationForStudyCommand request, CancellationToken cancellationToken)
        {
            var recommendations = await _appDbContext.RecommendationForStudies.ToListAsync();

            foreach (var recommendationForStudyDto in request.Data)
            {
                var existentRecommendation = recommendations.FirstOrDefault(s => s.Id == recommendationForStudyDto.Id);

                if (existentRecommendation == null)
                {

                    var item = _mapper.Map<RecommendationForStudy>(recommendationForStudyDto);

                    await _appDbContext.RecommendationForStudies.AddAsync(item);
                }
                else
                {
                    _mapper.Map(recommendationForStudyDto, existentRecommendation);

                    recommendations.Remove(existentRecommendation);
                }
            }

            if (recommendations.Any())
            {
                _appDbContext.RecommendationForStudies.RemoveRange(recommendations);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
