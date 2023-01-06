using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class TestQuestionTestAnswer : SoftDeleteBaseEntity
    {
        public int TestQuestionId { get; set; }
        public TestQuestion TestQuestion { get; set; }
        public int TestAnswerId { get; set; }
        public TestAnswer TestAnswer { get; set; }
    }
}
