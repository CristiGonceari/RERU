using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CODWER.RERU.Personal.Data.Persistence.Initializer
{
    public static class DatabaseSeeder
    {
        public static void SeedDb(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();

            AddBaseNomenclatureTypes(appDbContext);
        }

        private static void AddBaseNomenclatureTypes(AppDbContext appDbContext)
        {
            if (!appDbContext.NomenclatureTypes.Any(x => x.BaseNomenclature == BaseNomenclatureTypesEnum.BloodTypes))
            {
                appDbContext.NomenclatureTypes.Add(new NomenclatureType
                {
                    Name = "Grupa Sanguina",
                    BaseNomenclature = BaseNomenclatureTypesEnum.BloodTypes,
                    IsActive = true
                });
            }

            if (!appDbContext.NomenclatureTypes.Any(x => x.BaseNomenclature == BaseNomenclatureTypesEnum.Currency))
            {
                appDbContext.NomenclatureTypes.Add(new NomenclatureType
                {
                    Name = "Valuta",
                    BaseNomenclature = BaseNomenclatureTypesEnum.Currency,
                    IsActive = true
                });
            }

            if (!appDbContext.NomenclatureTypes.Any(x => x.BaseNomenclature == BaseNomenclatureTypesEnum.Rank))
            {
                appDbContext.NomenclatureTypes.Add(new NomenclatureType
                {
                    Name = "Grade militare",
                    BaseNomenclature = BaseNomenclatureTypesEnum.Rank,
                    IsActive = true
                });
            }

            if (!appDbContext.NomenclatureTypes.Any(x => x.BaseNomenclature == BaseNomenclatureTypesEnum.FamilyComponent))
            {
                appDbContext.NomenclatureTypes.Add(new NomenclatureType
                {
                    Name = "Relatii de rudenie",
                    BaseNomenclature = BaseNomenclatureTypesEnum.FamilyComponent,
                    IsActive = true
                });
            }

            if (!appDbContext.NomenclatureTypes.Any(x => x.BaseNomenclature == BaseNomenclatureTypesEnum.StudyType))
            {
                appDbContext.NomenclatureTypes.Add(new NomenclatureType
                {
                    Name = "Tipuri de studii",
                    BaseNomenclature = BaseNomenclatureTypesEnum.StudyType,
                    IsActive = true
                });
            }

            appDbContext.SaveChanges();
        }
    }

}
