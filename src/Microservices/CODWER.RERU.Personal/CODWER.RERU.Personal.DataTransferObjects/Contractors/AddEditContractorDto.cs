using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class AddEditContractorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }

        public int ContractorId { get; set; }

        public string PhoneNumber { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        //public string Idnp { get; set; }

        public DateTime BirthDate { get; set; }
        public int? BloodTypeId { get; set; } // nomenclature

        public SexTypeEnum Sex { get; set; }
        public StateLanguageLevel StateLanguageLevel { get; set; }


        public int CandidateNationalityId { get; set; }

        public int CandidateCitizenshipId { get; set; }
    }
}
