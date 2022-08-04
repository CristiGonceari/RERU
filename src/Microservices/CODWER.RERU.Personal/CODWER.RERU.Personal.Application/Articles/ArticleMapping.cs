using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
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
