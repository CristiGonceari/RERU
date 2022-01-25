using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Attestations
{
    public class AttestationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
    }
}
