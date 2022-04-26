using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.DataTransferObjects.UserProfiles
{
    public class FilterUserProfilesDto
    {
        public string Keyword { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
        public bool? Status { get; set; }
    }
}
