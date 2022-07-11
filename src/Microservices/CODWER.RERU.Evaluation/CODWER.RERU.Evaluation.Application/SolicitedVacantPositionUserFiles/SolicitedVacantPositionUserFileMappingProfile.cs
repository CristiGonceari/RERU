using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.StorageService.Entities;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles
{
    public class SolicitedVacantPositionUserFileMappingProfile : Profile
    {
        public SolicitedVacantPositionUserFileMappingProfile() 
        {
            CreateMap<File, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => $"{op.FileName}"));
        }
    }
}
