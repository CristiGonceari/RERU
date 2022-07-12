﻿using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps
{
    public class RegistrationFluxStepDto
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public RegistrationFluxStepEnum Step { get; set; }
        public int UserProfileId { get; set; }
    }
}
