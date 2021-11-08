using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;

namespace CODWER.RERU.Evaluation.Application.Options
{
    public class OptionMapping :Profile
    {
        public OptionMapping()
        {
            CreateMap<Option, OptionDto>();

            CreateMap<AddEditOptionDto, Option>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
