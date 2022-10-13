using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<ArticleEvaluation, ArticleEvaluationDto>()
                .ForMember(x => x.ContainsMedia, opts => opts.MapFrom(x => x.MediaFileId != null));

            CreateMap<ArticleEvaluationDto, ArticleEvaluation>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<ArticleEvaluation, EditArticleEvaluationDto>();
            CreateMap<EditArticleEvaluationDto, ArticleEvaluation>();

            CreateMap<ModuleRole, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Name));
        }
    }
}
