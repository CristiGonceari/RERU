using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Instructions
{
    public class AddEditInstructionDto
    {
        public int Id { get; set; }

        public string Thematic { get; set; }
        public string InstructorName { get; set; }
        public string InstructorLastName { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }

        public int ContractorId { get; set; }
    }
}
