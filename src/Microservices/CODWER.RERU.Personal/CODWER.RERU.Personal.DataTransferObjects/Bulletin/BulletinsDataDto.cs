using System;
using CODWER.RERU.Personal.DataTransferObjects.Address;

namespace CODWER.RERU.Personal.DataTransferObjects.Bulletin
{
    public class BulletinsDataDto
    {
        public int Id { get; set; }
        public string Series { get; set; }
        public DateTime ReleaseDay { get; set; }
        public string EmittedBy { get; set; }
        public int ContractorId { get; set; }

        public AddressDto BirthPlace { get; set; }
        public AddressDto ParentsResidenceAddress { get; set; }
        public AddressDto ResidenceAddress { get; set; }
    }
}
