using System;

namespace CODWER.RERU.Personal.DataTransferObjects.FamilyComponents
{
    public class FamilyMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public int RelationId { get; set; }
        public string RelationName { get; set; }
    }
}
