using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using RERU.Data.Entities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentGenerator
{
    public class DocumentGeneratorMappingProfile: Profile
    {
        public DocumentGeneratorMappingProfile()
        {
            CreateMap<DocumentTemplateGeneratorDto, DocumentTemplate>()
                .ForMember(x => x.Id,
                    opts => opts.Ignore());

            CreateMap<DocumentTemplate, DocumentTemplateGeneratorDto>();
        }
    }
}
