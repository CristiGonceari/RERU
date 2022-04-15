using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<ArticleEvaluation, ArticleEvaluationDto>();

            CreateMap<ArticleEvaluationDto, ArticleEvaluation>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
