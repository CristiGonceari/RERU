using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.DataTransferObjects.Instructions;

namespace CODWER.RERU.Personal.Application.Instructions
{
    public class InstructionMappingProfile : Profile
    {
        public InstructionMappingProfile()
        {
            CreateMap<AddEditInstructionDto, Instruction>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Instruction, AddEditInstructionDto>();
        }
    }
}
