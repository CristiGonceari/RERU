using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions
{
    public class RequiredDocumentPositionMappings : Profile
    {
        public RequiredDocumentPositionMappings()
        {
            CreateMap<RequiredDocumentPosition, AddEditRequiredDocumentPositionDto>();
            CreateMap<AddEditRequiredDocumentPositionDto, RequiredDocumentPosition>()
                .ForMember(x => x.Id, opts => opts.Ignore());
            ;

            CreateMap<RequiredDocumentPosition, RequiredDocumentPositionDto>();
        }
        
    }
}
