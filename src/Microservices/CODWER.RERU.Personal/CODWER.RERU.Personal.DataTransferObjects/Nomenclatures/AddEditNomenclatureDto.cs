using CODWER.RERU.Personal.DataTransferObjects.Nomenclatures.NomenclatureTypes;

namespace CODWER.RERU.Personal.DataTransferObjects.Nomenclatures
{
    public class AddEditNomenclatureDto
    {
        public BloodTypeDto BloodType { get; set; }
        public LanguageDto Language { get; set; }
        public LanguageLevelDto LanguageLevel { get; set; }
        public DriverLicenseCategoryDto DriverLicense { get; set; }
        public NationalityDto Nationality { get; set; }
        public VacationTypeDto VacationType { get; set; }
    }
}
