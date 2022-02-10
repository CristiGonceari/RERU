using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Reports;

namespace CODWER.RERU.Personal.Application.Reports
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<ContractorFile, ReportItemDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.FileId))
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(op => op.Contractor.Id))
                .ForMember(x => x.ContractorName,
                    opts => opts.MapFrom(op =>
                        $"{op.Contractor.LastName} {op.Contractor.FirstName} {op.Contractor.FatherName}"))
                .ForMember(x => x.ContractorLastName, opts => opts.MapFrom(op => op.Contractor.LastName))
                .ForMember(x => x.ContractorFatherName, opts => opts.MapFrom(op => op.Contractor.FatherName));
        }
    }
}
