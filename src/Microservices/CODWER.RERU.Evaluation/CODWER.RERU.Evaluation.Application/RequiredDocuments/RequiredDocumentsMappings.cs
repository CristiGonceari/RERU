using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments
{
    public class RequiredDocumentsMappings : Profile
    {
        public RequiredDocumentsMappings()
        {
            CreateMap<RequiredDocument, AddEditRequiredDocumentsDto>();
            CreateMap<AddEditRequiredDocumentsDto, RequiredDocument>()
                .ForMember(x => x.Id, opts => opts.Ignore());
                ;

            CreateMap<RequiredDocument, RequiredDocumentDto>();

            CreateMap<RequiredDocument, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(e => e.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(e => e.Name));
        }
    }
}
