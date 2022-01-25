using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;

namespace CODWER.RERU.Personal.Application.Attestations
{
    public class AttestationMappingProfile : Profile
    {
        public AttestationMappingProfile()
        {
            CreateMap<AddEditAttestationDto, Attestation>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore());

            CreateMap<Attestation, AttestationDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()));
        }
    }
}
