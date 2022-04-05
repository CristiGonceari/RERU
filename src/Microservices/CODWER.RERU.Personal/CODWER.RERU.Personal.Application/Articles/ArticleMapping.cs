using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.DataTransferObjects.Articles;

namespace CODWER.RERU.Personal.Application.Articles
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
