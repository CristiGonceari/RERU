using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using RERU.Data.Entities.Documents;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace RERU.Data.Persistence.Initializer
{
    public static class DatabaseSeeder
    {
        public static void SeedDb(AppDbContext appDbContext)
        {
            AddBaseDocumentTemplateKeys(appDbContext);
        }

        private static void AddBaseDocumentTemplateKeys(AppDbContext appDbContext)
        {
            
            var newValues = new List<DocumentTemplateKey>()
            {
                //TestType TestTemplate
                new DocumentTemplateKey { KeyName="{today_date_key_key}", Description= "Data pentru ziua de azi", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{test_template_name_key}", Description= "Numele șablonului de test", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{test_template_category_name_key}", Description= "Numarul de categorii din șablonul de test", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{rules_key}", Description= "Reguli", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{question_count_key}", Description= "Numarul de intrebari", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{min_percent_key}", Description= "Punctajul Minim", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{duration_key}", Description= "Durata", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{settings_max_errors_key}", Description= "Maxim posibile erori", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{settings_formula_for_one_answer_key}", Description= "Formula pentru intrebari cu un raspuns", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{settings_formula_for_multiple_answer_key}", Description= "Formula pentru intrebari cu respunsuri multiple", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{status_key}", Description= "Statutul șablonului de test", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{mode_key}", Description= "Modul șablonului de test", FileType = FileTypeEnum.TestTemplate },
                new DocumentTemplateKey { KeyName="{categories_sequence_key}", Description= "Ordinea întrebărilor in test", FileType = FileTypeEnum.TestTemplate },

                //TestType Test
                new DocumentTemplateKey { KeyName="{today_date_key}", Description= "Data pentru ziua de azi", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{accumulated_percentage_key}", Description= "Punctaj Acumulat", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{max_errors_key}", Description= "Maxim posibile erori", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{test_pass_status_key}", Description= "Statutul testului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{result_status_key}", Description= "Rezultatul testului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{programmed_time_key}", Description= "Data programata pentru inceperea testului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{start_time_key}", Description= "Data cand sa inceput testului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{end_time_key}", Description= "Data cand sa sfarsit testului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{event_name_key}", Description= "Numele evenimentul la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{event_description_key}", Description= "Detalii despre evenimentul la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{event_from_date_key}", Description= "Data de incepere a evenimentul la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{event_till_date_key}", Description= "Data de incheiere a evenimentul la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{location_name_key}", Description= "Numele locatiei la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{location_description_key}", Description= "Detalii despre locatia la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{location_address_key}", Description= "Adresa locatiei la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{location_type_key}", Description= "Tipul locatiei la care este atasat testul", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{location_places_key}", Description= "Numarul de locuri ale locatiei la care este atasat testul", FileType = FileTypeEnum.Test },

                        //Evaluated person
                new DocumentTemplateKey { KeyName="{appraiser_name_key}", Description= "Numele evaluatului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{appraiser_last_name_key}", Description= "Prenumele evaluatului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{appraiser_father_name_key}", Description= "Patronimicul evaluatului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{appraiser_idnp_key}", Description= "IDNP din buletinul evaluatului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{appraiser_email_key}", Description= "E-mailul evaluatului", FileType = FileTypeEnum.Test },
                        //Evaluator
                new DocumentTemplateKey { KeyName="{evaluator_name_key}", Description= "Numele evaluatorului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{evaluator_last_name_key}", Description= "Prenumele evaluatorului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{evaluator_father_name_key}", Description= "Patronimicul evaluatorului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{evaluator_idnp_key}", Description= "IDNP din buletinul evaluatorului", FileType = FileTypeEnum.Test },
                new DocumentTemplateKey { KeyName="{evaluator_email_key}", Description= "E-mailul evaluatorului", FileType = FileTypeEnum.Test },
            };

            var keys = appDbContext.DocumentTemplateKeys.ToList(); 

            foreach (var item in newValues)
            {
                var existentKey = keys.FirstOrDefault(k => k.KeyName == item.KeyName);

                if (existentKey == null)
                {
                    appDbContext.DocumentTemplateKeys.Add(item);
                }
                else 
                {
                    keys.Remove(existentKey);
                }
            }

            if (keys.Any())
            {
                appDbContext.DocumentTemplateKeys.RemoveRange(keys);
            }

            appDbContext.SaveChanges();
        }
    }
}
