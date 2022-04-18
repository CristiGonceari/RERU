using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Locations
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public TestingLocationType Type { get; set; }
        public int Places { get; set; }
        public string Description { get; set; }
    }
}
