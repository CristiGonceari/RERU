using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.TestImportProcesses
{
    public class TestImportProcessMapping : Profile
    {
        public TestImportProcessMapping()
        {
            CreateMap<Process, HistoryProcessDto>()
                .ForMember(x => x.StartTime, opts => opts.MapFrom(src => src.CreateDate))
                .ForMember(x => x.EndTime, opts => opts.MapFrom(src => src.UpdateDate))
                ;
        }
    }
}
