using System;

namespace CODWER.RERU.Personal.DataTransferObjects.ContractorEvents
{
    public class AddEditContractorEventDto
    {
        public int Id { get; set; }
        public int Discriminator { get; set; }
        public DateTime Date { get; set; }
        public int ContractorId { get; set; }
        public int NewDepartmentId { get; set; }
    }
}
