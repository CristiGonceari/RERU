using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles
{
    public class ArticleMappings : Profile
    {
        public ArticleMappings()
        {
            CreateMap<ArticleEv360, ArticleEv360Dto>()
                .ForMember(x => x.ContainsMedia, opts => opts.MapFrom(x => x.MediaFileId != null));

            CreateMap<ArticleEv360Dto, ArticleEv360>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<EditArticleEv360Dto, ArticleEv360>();

            CreateMap<ModuleRole, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Name));
        }
    }
}
