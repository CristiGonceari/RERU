using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using RERU.Data.Entities.Documents;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates
{
    public class DocumentTemplateMappingProfile : Profile
    {
        public DocumentTemplateMappingProfile()
        {
            CreateMap<AddEditDocumentTemplateDto, DocumentTemplate>()
                .ForMember(x => x.Id,
                    opts => opts.Ignore());

            CreateMap<DocumentTemplate, AddEditDocumentTemplateDto>();

            CreateMap<DocumentTemplateKey, DocumentTemplateKeyDto>();
            CreateMap<DocumentTemplateKeyDto, DocumentTemplateKey>();

        }
    }
}
