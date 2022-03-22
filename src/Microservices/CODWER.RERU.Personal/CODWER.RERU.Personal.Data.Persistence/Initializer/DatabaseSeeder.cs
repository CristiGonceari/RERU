using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            if (!appDbContext.DocumentTemplateCategories.Any(dc => dc.Id >= 1))
            {
                appDbContext.DocumentTemplateCategories.AddRange(new List<DocumentTemplateCategory>()
                {
                     new DocumentTemplateCategory
                    {
                            Id = 1,
                            Name = "Angajat",
                    },
                     new DocumentTemplateCategory
                    {
                            Id = 2,
                            Name = "Companie",
                     }
                }
                ); 
            }

            if (!appDbContext.DocumentTemplateKeys.Any(dc => dc.Id >= 1)) 
            {
                appDbContext.DocumentTemplateKeys.AddRange(new List<DocumentTemplateKey>()
                    {
                        new DocumentTemplateKey {Id = 1, KeyName = "{today_date_key}", Description = "Data pentru ziua de azi", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 2, KeyName = "{c_name_key}", Description = "Numele Angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 3, KeyName = "{c_last_name_key}", Description = "Prenumele Angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 4, KeyName = "{c_father_name_key}", Description = "Patronimicul Angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 5, KeyName = "{c_idnp_key}", Description = "IDNP din buletin", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 6, KeyName = "{c_bulletin_series_key}", Description = "Seria buletinului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 7, KeyName = "{c_bulletin_release_by_key}", Description = "Buletinul angajatului a fost emis de", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 8, KeyName = "{c_bulletin_release_day_key}", Description = "Data emiterii buletinului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 9, KeyName = "{c_birthday_key}", Description = "Ziua de nastere a angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 10, KeyName = "{c_work_place_key}", Description = "Locul de munca a angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 11, KeyName = "{c_employment_date_key}", Description = "Ziua angajarii", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 12, KeyName = "{c_work_hours_key}", Description = "Numarul ore de munca pe zi conform contractului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 13, KeyName = "{c_salary_key}", Description = "Salariu angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 14, KeyName = "{c_sex_type_key}", Description = "Sexul angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 15, KeyName = "{c_role_key}", Description = "Rolul angajatului in companie", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 16, KeyName = "{c_dissmisal_date_key}", Description = "Data demisionarii angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 17, KeyName = "{c_internship_days_key}", Description = "Zilele de stagiere a angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 18, KeyName = "{c_address_key}", Description = "Adresa de locuinta a angajatului", DocumentCategoriesId= 1},
                        new DocumentTemplateKey {Id = 19, KeyName = "{company_key}", Description = "Numele companiei", DocumentCategoriesId= 2} ,
                        new DocumentTemplateKey {Id = 20, KeyName = "{company_post_code_key}", Description = "Codul postal al companiei", DocumentCategoriesId= 2},
                        new DocumentTemplateKey {Id = 21, KeyName = "{company_city_key}", Description = "Orasul de locatiune al compamiei", DocumentCategoriesId= 2},
                        new DocumentTemplateKey {Id = 22, KeyName = "{company_street_key}", Description = "Adresa de locatie a companiei", DocumentCategoriesId= 2},
                        new DocumentTemplateKey {Id = 23, KeyName = "{company_idno_key}", Description = "IDNO-ul companiei", DocumentCategoriesId= 2},
                        new DocumentTemplateKey {Id = 24, KeyName = "{director_name_key}", Description = "Numele directorului", DocumentCategoriesId= 2},
                        new DocumentTemplateKey {Id = 25, KeyName = "{director_last_name_key}", Description = "Prenumele directorului", DocumentCategoriesId= 2},
                        new DocumentTemplateKey {Id = 26, KeyName = "{minister_srl_key}", Description = "Tipul companiei", DocumentCategoriesId= 2},
                    }
                 );
            };

            appDbContext.SaveChanges();
        }
    }

}
