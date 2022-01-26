using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Bulletins.GetContractorBulletin;
using CODWER.RERU.Personal.Application.Contractors.GetContractorAvatar;
using CODWER.RERU.Personal.Application.Contractors.GetContractorFiles;
using CODWER.RERU.Personal.Application.Contractors.GetContractorPermissions;
using CODWER.RERU.Personal.Application.Profiles.ContractorCim.GetContractorCim;
using CODWER.RERU.Personal.Application.Profiles.ContractorFamilyMembers.GetContractorFamilyMembers;
using CODWER.RERU.Personal.Application.Profiles.ContractorPositions.GetContractorPositions;
using CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorProfile;
using CODWER.RERU.Personal.Application.Profiles.ContractorRanks.GetContractorRanks;
using CODWER.RERU.Personal.Application.Profiles.ContractorStudies.GetContractorStudies;
using CODWER.RERU.Personal.Application.Profiles.ContractorTimeSheetTable;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CODWER.RERU.Personal.DataTransferObjects.Profiles;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers.Profile
{
    [Route("api/profile/[controller]")]
    [ApiController]
    public class ContractorProfileController : BaseController
    {
        [HttpGet("permissions")]
        public async Task<ContractorLocalPermissionsDto> GetPermissionsData()
        {
            return await Mediator.Send(new GetContractorPermissionsQuery());
        }

        [HttpGet("profile")]
        public async Task<ContractorProfileDto> GetGeneralData()
        {
            return  await Mediator.Send(new GetContractorProfileQuery());
        }

        [HttpGet("profile/Avatar")]
        public async Task<ContractorAvatarDetailsDto> GetAvatar()
        {
            return await Mediator.Send(new GetContractorAvatarQuery());
        }

        [HttpGet("bulletin")]
        public async Task<BulletinsDataDto> GetBulletinData()
        {
            return await Mediator.Send(new GetContractorBulletinQuery());
        }

        [HttpGet("contract")]
        public async Task<IndividualContractDetails> GetContractData()
        {
            return await Mediator.Send(new GetContractorCimQuery());
        }

        [HttpGet("positions")]
        public async Task<PaginatedModel<PositionDto>> GetPositionsData([FromQuery] GetContractorPositionsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("ranks")]
        public async Task<PaginatedModel<RankDto>> GetRanksData([FromQuery] GetContractorRanksQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("studies")]
        public async Task<PaginatedModel<StudyDataDto>> GetStudiesData([FromQuery] GetContractorStudiesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("family-members")]
        public async Task<PaginatedModel<FamilyMemberDto>> GetFamilyMembers([FromQuery] GetContractorFamilyMembersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("files")]
        public async Task<PaginatedModel<FileNameDto>> GetContractor([FromQuery] GetContractorFilesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("time-sheet-table")]
        public async Task<ContractorProfileTimeSheetTableDto> GetTimeSheetTable([FromQuery] GetTimeSheetTableQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }
    }
}