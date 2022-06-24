using RERU.Data.Entities;

namespace CODWER.RERU.Core.DataTransferObjects.MaterialStatus
{
    public class MaterialStatusDto
    {
        public int Id { get; set; }
        public MaterialStatusType MaterialStatusType { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
