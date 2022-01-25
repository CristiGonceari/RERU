using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class ContractorSelectItem : SelectItem
    {
        public List<KeyValuePair<string,string>> Properties { get; set; }
    }
}
