using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.Articles;

namespace CODWER.RERU.Core.Application.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<Article, ArticleDto>();

            CreateMap<ArticleDto, Article>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
