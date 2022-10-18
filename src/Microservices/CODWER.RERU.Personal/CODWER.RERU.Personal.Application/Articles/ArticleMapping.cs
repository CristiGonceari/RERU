using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(x => x.ContainsMedia, opts => opts.MapFrom(x => x.MediaFileId != null));

            CreateMap<ArticleDto, Article>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<EditArticlePersonalDto, Article>();

            CreateMap<UserProfile, UserProfileDto>();

            CreateMap<ModuleRole, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Name));
        }
    }
}
