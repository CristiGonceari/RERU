using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.DataTransferObjects.Documents;

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
        }
    }
}
