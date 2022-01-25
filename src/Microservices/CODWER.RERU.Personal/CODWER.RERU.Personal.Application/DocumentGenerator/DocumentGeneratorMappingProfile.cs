using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.DataTransferObjects.Documents;

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
