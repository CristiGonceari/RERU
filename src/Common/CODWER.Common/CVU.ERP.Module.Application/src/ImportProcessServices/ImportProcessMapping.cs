using AutoMapper;
using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;
using RERU.Data.Entities;

namespace CVU.ERP.Module.Application.ImportProcessServices
{
    public class ImportProcessMapping : Profile
    {
        public ImportProcessMapping()
        {
            CreateMap<Process, ProcessDataDto>();
        }
    }
}
