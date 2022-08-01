using System;
using System.Collections.Generic;
using System.Text;

namespace CVU.ERP.Common.DataTransferObjects.Users
{
    public class AddContractorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }

        public int BloodTypeId { get; set; } // nomenclature
    }
}
