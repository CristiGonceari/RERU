using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Reports;

namespace CODWER.RERU.Personal.Application.Reports
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<ByteArrayFile, ReportItemDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.FileName))
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(op => op.Contractor.Id))
                .ForMember(x => x.ContractorName, 
                opts => opts.MapFrom(op => $"{op.Contractor.LastName} {op.Contractor.FirstName} {op.Contractor.FatherName}"));
        }
    }
}
