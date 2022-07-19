using System;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.DismissalRequests
{
    public class MyDismissalRequestDto
    {
        public int Id { get; set; }

        public DateTime From { get; set; }
        public StageStatusEnum Status { get; set; }

        public string RequestId { get; set; }
        public string RequestName { get; set; }

        public string OrderId { get; set; }
        public string OrderName { get; set; }

        public int PositionId { get; set; }
        public string PositionOrganizationRoleName { get; set; }
    }
}
