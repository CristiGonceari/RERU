using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Reports;
using CVU.ERP.StorageService.Entities;

namespace CODWER.RERU.Personal.Application.Reports
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<File, ReportItemDto>()
                .ForMember(x => x.CreateDate, opts => opts.MapFrom(op => op.CreateDate))
                .ForMember(x => x.FileName, opts => opts.MapFrom(op => op.FileName))
                .ForMember(x => x.Type, opts => opts.MapFrom(op => op.FileType))
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.Id.ToString()));
        }
    }
}
