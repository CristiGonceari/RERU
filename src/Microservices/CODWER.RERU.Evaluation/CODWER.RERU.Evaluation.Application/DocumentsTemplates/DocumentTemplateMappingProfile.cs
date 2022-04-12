using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.Documents;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;

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
