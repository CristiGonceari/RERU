using System.Collections.Generic;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class ContractorDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string EmployerState { get; set; }
        public string Idnp { get; set; }

        //position fields
        //public int PositionId { get; set; }
        //public DateTime? FromDate { get; set; }
        //public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        //public int? OrganizationRoleId { get; set; }
        public string OrganizationRoleName { get; set; }
        public List<ContactRowDto> Contacts { get; set; }


        //public string PersonalNumber { get; set; }

        //public string FatherName { get; set; }
        //public DateTime BirthDate { get; set; }
        //public SexTypeEnum Sex { get; set; }
        //public string SexName { get; set; }

        //public int BirthPlaceId { get; set; }
        //public string BirthPlaceCountry { get; set; }
        //public string BirthPlaceCity { get; set; }

        //public AddressDto BirthPlace { get; set; }
        //public int LivingAddressId { get; set; }
        //public string LivingAddressCountry { get; set; }
        //public string LivingAddressCity { get; set; }
        //public AddressDto LivingAddress { get; set; }

        //public int ContractorTypeId { get; set; }
        //public string ContractorTypeName { get; set; }
        //public int NationalityId { get; set; }
        //public string NationalityName { get; set; }
        //public int BloodTypeId { get; set; }
        //public string BloodTypeName { get; set; }
    }
}
