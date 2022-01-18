using AutoMapper;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Logging.Entities;

namespace CODWER.RERU.Logging.Application.LoggingMappings
{
    public class LoggingMapping : Profile 
    {
        public LoggingMapping()
        {
            CreateMap<Log, LogDto>();
        }
    }
}
