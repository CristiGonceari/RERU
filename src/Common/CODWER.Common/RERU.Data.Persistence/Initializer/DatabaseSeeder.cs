using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using RERU.Data.Entities;
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
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_azi}", Description= "Data pentru ziua de azi", FileType = FileTypeEnum.TestTemplate, TranslateId = 1 },
                new DocumentTemplateKey { KeyName="{cheia_numelui_șablonului_de_testare}", Description= "Numele șablonului de test", FileType = FileTypeEnum.TestTemplate, TranslateId = 22 },
                new DocumentTemplateKey { KeyName="{cheia_numelui_categoriei_șablonului_de_testare}", Description= "Numarul de categorii din șablonul de test", FileType = FileTypeEnum.TestTemplate, TranslateId = 23 },
                new DocumentTemplateKey { KeyName="{cheia_regulilor}", Description= "Reguli", FileType = FileTypeEnum.TestTemplate, TranslateId = 24 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_total_de_întrebări}", Description= "Numarul de intrebari", FileType = FileTypeEnum.TestTemplate, TranslateId = 25 },
                new DocumentTemplateKey { KeyName="{cheie_minim_punctaj}", Description= "Punctajul Minim", FileType = FileTypeEnum.TestTemplate, TranslateId = 26 },
                new DocumentTemplateKey { KeyName="{cheie_de_durată}", Description= "Durata", FileType = FileTypeEnum.TestTemplate, TranslateId = 27 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_de_maxim_posibile_erori}", Description= "Maxim posibile erori", FileType = FileTypeEnum.TestTemplate, TranslateId = 28 },
                new DocumentTemplateKey { KeyName="{cheie_cu_formula_pentru_un_singur_raspuns}", Description= "Formula pentru intrebari cu un raspuns", FileType = FileTypeEnum.TestTemplate, TranslateId = 29 },
                new DocumentTemplateKey { KeyName="{cheie_cu_formula_pentru_răspunsuri_multiple}", Description= "Formula pentru intrebari cu respunsuri multiple", FileType = FileTypeEnum.TestTemplate, TranslateId = 30 },
                new DocumentTemplateKey { KeyName="{cheie_cu_statutul_șablonului}", Description= "Statutul șablonului de test", FileType = FileTypeEnum.TestTemplate, TranslateId = 31 },
                new DocumentTemplateKey { KeyName="{cheie_modul_șablonului}", Description= "Modul șablonului de test", FileType = FileTypeEnum.TestTemplate, TranslateId = 32 },
                new DocumentTemplateKey { KeyName="{cheie_cu_ordinea_întrebărilor_in_test}", Description= "Ordinea întrebărilor in test", FileType = FileTypeEnum.TestTemplate, TranslateId = 33 },

                //TestType Test
                new DocumentTemplateKey { KeyName="{cheie_pentru_data_de_azi}", Description= "Data pentru ziua de azi", FileType = FileTypeEnum.Test, TranslateId = 34 },
                new DocumentTemplateKey { KeyName="{cheie_cu_punctaj_acumulat}", Description= "Punctaj Acumulat", FileType = FileTypeEnum.Test, TranslateId = 35 },
                new DocumentTemplateKey { KeyName="{cheie_cu_maxim_posibile_erori}", Description= "Maxim posibile erori", FileType = FileTypeEnum.Test, TranslateId = 36 },
                new DocumentTemplateKey { KeyName="{cheie_cu_statutul_testului}", Description= "Statutul testului", FileType = FileTypeEnum.Test, TranslateId = 37 },
                new DocumentTemplateKey { KeyName="{cheie_cu_rezultatul_testului}", Description= "Rezultatul testului", FileType = FileTypeEnum.Test, TranslateId = 38 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_programata_a_testului}", Description= "Data programata pentru inceperea testului", FileType = FileTypeEnum.Test, TranslateId = 21 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_început_a_testului}", Description= "Data cand sa inceput testului", FileType = FileTypeEnum.Test, TranslateId = 20 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_încheiere_a_testului}", Description= "Data cand sa sfarsit testului", FileType = FileTypeEnum.Test, TranslateId = 19 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evenimentului}", Description= "Numele evenimentul la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 9 },
                new DocumentTemplateKey { KeyName="{cheie_cu_descrierea_evenimentului}", Description= "Detalii despre evenimentul la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 2 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_început_a_evenimentului}", Description= "Data de incepere a evenimentul la care este atasat testul", FileType = FileTypeEnum.Test,  TranslateId = 3 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_încheiere_a_evenimentului}", Description= "Data de incheiere a evenimentul la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 4 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_locatiei}", Description= "Numele locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 5 },
                new DocumentTemplateKey { KeyName="{cheie_cu_descrierea_locatiei}", Description= "Detalii despre locatia la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 6 },
                new DocumentTemplateKey { KeyName="{cheie_cu_adresa_locatiei}", Description= "Adresa locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 7 },
                new DocumentTemplateKey { KeyName="{cheie_cu_tipul_locatiei}", Description= "Tipul locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 8 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_de_locuri_ale_locatiei}", Description= "Numarul de locuri ale locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 10 },

                        //Evaluated person
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evaluatului}", Description= "Numele evaluatului", FileType = FileTypeEnum.Test, TranslateId = 18 },
                new DocumentTemplateKey { KeyName="{cheie_cu_prenumele_evaluatului}", Description= "Prenumele evaluatului", FileType = FileTypeEnum.Test, TranslateId = 11 },
                new DocumentTemplateKey { KeyName="{cheie_cu_patronimicul_evaluatului}", Description= "Patronimicul evaluatului", FileType = FileTypeEnum.Test, TranslateId = 12 },
                new DocumentTemplateKey { KeyName="{cheie_cu_IDNP_evaluatului}", Description= "IDNP din buletinul evaluatului", FileType = FileTypeEnum.Test, TranslateId = 13 },
                new DocumentTemplateKey { KeyName="{cheie_cu_email_evaluatului}", Description= "E-mailul evaluatului", FileType = FileTypeEnum.Test, TranslateId = 14 },
                        //Evaluator
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evaluatorului}", Description= "Numele evaluatorului", FileType = FileTypeEnum.Test, TranslateId = 15 },
                new DocumentTemplateKey { KeyName="{cheie_cu_prenumele_evaluatorului}", Description= "Prenumele evaluatorului", FileType = FileTypeEnum.Test, TranslateId = 16 },
                new DocumentTemplateKey { KeyName="{cheie_cu_patronimicul_evaluatorului}", Description= "Patronimicul evaluatorului", FileType = FileTypeEnum.Test, TranslateId = 17 },
                new DocumentTemplateKey { KeyName="{cheie_cu_IDNP_evaluatorului}", Description= "IDNP din buletinul evaluatorului", FileType = FileTypeEnum.Test, TranslateId = 39 },
                new DocumentTemplateKey { KeyName="{cheie_cu_email_evaluatorului}", Description= "E-mailul evaluatorului", FileType = FileTypeEnum.Test, TranslateId = 40 },
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

            AddRegistrationPageMessage(appDbContext);

            appDbContext.SaveChanges();
        }

        private static void AddRegistrationPageMessage(AppDbContext appDbContext)
        {
            var existMessage = appDbContext.RegistrationPageMessages.Any();

            if (!existMessage)
            {
                const string message = "<p style=\"text-align:center;\">&nbsp;</p>" +
                                       "<p style=\"text-align:center;\">&nbsp;</p>" +
                                       "<p style=\"text-align:center;\">" +
                                       "<span class=\"text-huge\">Pentru a vă înregistra în calitate de candidat, este necesar să completați formularul de mai jos.</span></p>" +
                                       "<p style=\"text-align:center;\"><span class=\"text-huge\">După înregistrare, vi se va expedia un email cu credențialele dvs.</span></p><p>&nbsp;</p>" +
                                       "<p><span class=\"text-big\">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Cu ajutorul acestor credențiale trebuie să vă autentificați. După autentificare mergeți la modulul „Evaluare și Testare”. (Pasul 1)</span></p>" +
                                       "<p>&nbsp;</p><p><span class=\"text-big\">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Solicitați un test din secțiunea „Activitățile mele”.(Pasul 2)</span></p>" +
                                       "<p>&nbsp;</p><p><span class=\"text-big\">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Opțiunea „Teste solicitate” (Pasul 3)</span></p><p>&nbsp;</p>" +
                                       "<p><span class=\"text-big\">&nbsp; &nbsp; &nbsp; Apăsați butonul „Solicită test” (Pasul 4) si completați formularul cu datele dorite (Pasul 5)</span></p>" +
                                       "<ul>" +
                                       "<li><span class=\"text-big\">&nbsp;- Eveniment</span></li>" +
                                       "<li><span class=\"text-big\">- Test</span></li>" +
                                       "<li><span class=\"text-big\">- Timpul comod</span></li>" +
                                       "</ul>" +
                                       "<p><span class=\"text-big\">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Apasăti butonul „Confirmă” (Pasul 6) pentru a finaliza procedura.</span></p><p>&nbsp;" +
                                       "</p>";

                var record = new RegistrationPageMessage
                {
                    Message = message
                };

                appDbContext.RegistrationPageMessages.Add(record);
            }
        }
    }
}
