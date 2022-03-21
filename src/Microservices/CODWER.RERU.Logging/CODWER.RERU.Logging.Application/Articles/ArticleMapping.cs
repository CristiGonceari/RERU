using AutoMapper;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Logging.Entities;

namespace CODWER.RERU.Logging.Application.Articles
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
