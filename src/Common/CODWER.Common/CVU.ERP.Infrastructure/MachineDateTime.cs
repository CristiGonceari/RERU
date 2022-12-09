using System;
using CVU.ERP.Common;

namespace CVU.ERP.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today => DateTime.Today;

        public int CurrentYear => DateTime.Now.Year;
    }
}
