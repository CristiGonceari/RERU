using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Processes;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Processes
{
    public class ProcessesMappings : Profile
    {
        public ProcessesMappings()
        {
            CreateMap<Process, HistoryProcessesDto>()
                .ForMember(x => x.StartTime, opts => opts.MapFrom(src => src.CreateDate))
                .ForMember(x => x.EndTime, opts => opts.MapFrom(src => src.UpdateDate))
                ;
        }
    }
}
