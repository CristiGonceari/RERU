using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using RERU.Data.Entities.Documents;
using RERU.Data.Entities.PersonalEntities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates
{
   public class DocumentTemplateMappingProfile : Profile
    {
        public DocumentTemplateMappingProfile()
        {
            CreateMap<AddEditDocumentTemplateDto, DocumentTemplate>()
                .ForMember(x => x.Id,
                    opts => opts.Ignore());
          
            CreateMap<DocumentTemplate, AddEditDocumentTemplateDto>();

            CreateMap<HrDocumentTemplateCategory, DocumentTemplateCategoryDto>()
                .ForMember(x => x.DocumentTemplateKeys, opts => opts.MapFrom(x => x.HrDocumentKeys));

            CreateMap<DocumentTemplateKey, DocumentTemplateKeyDto>();
            CreateMap<DocumentTemplateKeyDto, DocumentTemplateKey>();

        }
    }
}
