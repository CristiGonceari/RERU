using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class Address : SoftDeleteBaseEntity
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Apartment { get; set; }
        public string PostCode { get; set; }
    }
}
