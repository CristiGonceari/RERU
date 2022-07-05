﻿using CODWER.RERU.Core.DataTransferObjects.Address;
using System;

namespace CODWER.RERU.Core.DataTransferObjects.Bulletin
{
    public class BulletinDto
    {
        public int Id { get; set; }
        public string Series { get; set; }
        public DateTime ReleaseDay { get; set; }
        public string EmittedBy { get; set; }
        public string Idnp { get; set; }
        public int UserProfileId { get; set; }

        public AddressDto BirthPlace { get; set; }
        public AddressDto ParentsResidenceAddress { get; set; }
        public AddressDto ResidenceAddress { get; set; }
    }
}
