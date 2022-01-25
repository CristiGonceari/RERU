using System;
using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.DismissalRequests
{
    public class DismissalRequestDto
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public StageStatusEnum Status { get; set; }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorLastName { get; set; }

        public int? RequestId { get; set; }
        public string RequestName { get; set; }

        public int? OrderId { get; set; }
        public string OrderName { get; set; }

        public int PositionId { get; set; }
        public string PositionOrganizationRoleName { get; set; }
    }
}
