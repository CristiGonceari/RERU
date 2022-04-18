using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<ArticleCore, ArticleCoreDto>();

            CreateMap<ArticleCoreDto, ArticleCore>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
