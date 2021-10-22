using System;

namespace CODWER.RERU.Core.Data.Entities
{
    public class DocumentBody
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
    }
}
