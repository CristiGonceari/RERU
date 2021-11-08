using System;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Plans
{
    public class PlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime TillDate { get; set; }
    }
}
