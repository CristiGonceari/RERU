using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Internal
{
    public class TestMapping : Profile
    {
        public TestMapping()
        {
            CreateMap<Test, TestDataDto>()
                .ForMember(x => x.TestId, opts => opts.MapFrom(src => src.Id));
        }
    }
}