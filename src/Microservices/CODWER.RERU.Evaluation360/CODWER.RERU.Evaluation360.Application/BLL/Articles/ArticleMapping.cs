using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<ArticleCore, ArticleCoreDto>()
                .ForMember(x => x.ContainsMedia, opts => opts.MapFrom(x => x.MediaFileId != null));

            CreateMap<ArticleCoreDto, ArticleCore>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<EditArticleCoreDto, ArticleCore>();

            CreateMap<ModuleRole, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Name));
        }
    }
}
