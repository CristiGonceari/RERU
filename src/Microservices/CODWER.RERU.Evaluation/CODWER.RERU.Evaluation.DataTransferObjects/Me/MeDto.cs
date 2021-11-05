﻿using CVU.ERP.Module.Application.Models;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Me
{
    public class MeDto
    {
        public bool IsAuthenticated { set; get; }
        public ApplicationUser User { set; get; }
        public TenantDto Tenant { get; set; }
    }
}
