using CODWER.RERU.Core.DataTransferObjects.Users;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.Me {
    public class MeDto {
        public bool IsAuthenticated { set; get; }
        public ApplicationUserDto User { set; get; }
        public TenantDto Tenant { get; set; }
        public bool IsCandidateStatus { get; set; }
    }
} 