using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class TestTemplateSettings : SoftDeleteBaseEntity
    {
        public int TestTemplateId { get; set; }
        public bool StartWithoutConfirmation { get; set; }
        public bool StartBeforeProgrammation { get; set; }
        public bool StartAfterProgrammation { get; set; }
        public bool PossibleGetToSkipped { get; set; }
        public bool PossibleChangeAnswer { get; set; }
        public bool CanViewResultWithoutVerification { get; set; }
        public bool CanViewPollProgress { get; set; }
        public bool HidePagination { get; set; }
        public bool ShowManyQuestionPerPage { get; set; }
        public int QuestionsCountPerPage { get; set; }
        public int? MaxErrors { get; set; }
        public ScoreFormulaEnum FormulaForOneAnswer { get; set; }
        public bool? NegativeScoreForOneAnswer { get; set; }
        public ScoreFormulaEnum FormulaForMultipleAnswers { get; set; }
        public bool? NegativeScoreForMultipleAnswers { get; set; }
    }
}
