﻿using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Positions
{
    public class PositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorkPlace { get; set; }
        public string No { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? OrganizationRoleId { get; set; }
        public string OrganizationRoleName { get; set; }

        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int? ContractorId { get; set; }
        public string ContractorName { get; set; }

        public int OrderId { get; set; }
        public string OrderName { get; set; }
    }
}