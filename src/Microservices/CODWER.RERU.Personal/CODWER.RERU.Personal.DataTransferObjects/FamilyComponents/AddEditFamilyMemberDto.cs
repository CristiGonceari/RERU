using System;

namespace CODWER.RERU.Personal.DataTransferObjects.FamilyComponents
{
    public class AddEditFamilyMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public int RelationId { get; set; } // nomenclature
        public int ContractorId { get; set; }
    }
}
