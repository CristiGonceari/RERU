using AutoMapper;
using RERU.Data.Entities;

namespace CVU.ERP.Module.Application.ImportProcesses
{
    public class ImportProcessMapping : Profile
    {
        public ImportProcessMapping()
        {
            CreateMap<Process, ProcessDataDto>();
        }
    }
}
