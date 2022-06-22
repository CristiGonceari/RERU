using CVU.ERP.Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RERU.Data.Entities
{
    public class CandidateNationality : SoftDeleteBaseEntity
    {
        public string NationalityName { get; set; }
        public int TranslateId { get; set; }
    }
}
