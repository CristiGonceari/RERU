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
            AddCandidateCitizenship(appDbContext);
            AddSolicitedVacantPositionMessages(appDbContext);
            AddCandidateNationality(appDbContext);
            AddModernLaguages(appDbContext);
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
                new DocumentTemplateKey { KeyName="{cheie_cu_statutul_testului}", Description= "Statutul testului", FileType = FileTypeEnum.Test, TranslateId = 37 },
                new DocumentTemplateKey { KeyName="{cheie_cu_rezultatul_testului}", Description= "Rezultatul testului", FileType = FileTypeEnum.Test, TranslateId = 38 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_programata_a_testului}", Description= "Data programata pentru inceperea testului", FileType = FileTypeEnum.Test, TranslateId = 21 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_inceput_a_testului}", Description= "Data cand sa inceput testului", FileType = FileTypeEnum.Test, TranslateId = 20 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evenimentului}", Description= "Numele evenimentul la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 9 },
                new DocumentTemplateKey { KeyName="{cheie_cu_descrierea_evenimentului}", Description= "Detalii despre evenimentul la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 2 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_inceput_a_evenimentului}", Description= "Data de incepere a evenimentul la care este atasat testul", FileType = FileTypeEnum.Test,  TranslateId = 3 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_incheiere_a_evenimentului}", Description= "Data de incheiere a evenimentul la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 4 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_locatiei}", Description= "Numele locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 5 },
                new DocumentTemplateKey { KeyName="{cheie_cu_descrierea_locatiei}", Description= "Detalii despre locatia la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 6 },
                new DocumentTemplateKey { KeyName="{cheie_cu_adresa_locatiei}", Description= "Adresa locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 7 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_de_locuri_ale_locatiei}", Description= "Numarul de locuri ale locatiei la care este atasat testul", FileType = FileTypeEnum.Test, TranslateId = 10 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_testului}", Description= "Numele Testului", FileType = FileTypeEnum.Test, TranslateId = 41 },

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

        private static void AddCandidateNationality(AppDbContext appDbContext)
        {
            var newCandidateNationality = new List<CandidateNationality>()
            {
                new CandidateNationality { NationalityName ="Afgană(Afgan)" , TranslateId = 1 },
                new CandidateNationality { NationalityName ="Afgană" , TranslateId = 2 },
                new CandidateNationality { NationalityName ="Albaneză(Albanez)" , TranslateId = 3 },
                new CandidateNationality { NationalityName ="Algerian" , TranslateId = 4 },
                new CandidateNationality { NationalityName ="Americană(American)" , TranslateId = 5 },
                new CandidateNationality { NationalityName ="Arabă" , TranslateId = 6 },
                new CandidateNationality { NationalityName ="Armeană" , TranslateId = 7 },
                new CandidateNationality { NationalityName ="Aromân" , TranslateId = 8 },
                new CandidateNationality { NationalityName ="Aromână" , TranslateId = 9 },
                new CandidateNationality { NationalityName ="Austriac" , TranslateId = 10 },
                new CandidateNationality { NationalityName ="Azer(azeră)" , TranslateId = 11 },
                new CandidateNationality { NationalityName ="belgiană" , TranslateId = 12 },
                new CandidateNationality { NationalityName ="britanic" , TranslateId = 13 },
                new CandidateNationality { NationalityName ="bulgară(bulgar)" , TranslateId = 14 },
                new CandidateNationality { NationalityName ="calabreză" , TranslateId = 15 },
                new CandidateNationality { NationalityName ="canadian" , TranslateId = 16 },
                new CandidateNationality { NationalityName ="catalană(catalan)" , TranslateId = 17 },
                new CandidateNationality { NationalityName ="cehă(ceh)" , TranslateId = 18 },
                new CandidateNationality { NationalityName ="cehoaică" , TranslateId = 19 },
                new CandidateNationality { NationalityName ="chineză(chinez)" , TranslateId = 20 },
                new CandidateNationality { NationalityName ="chinezoaică" , TranslateId = 21 },
                new CandidateNationality { NationalityName ="coreeană(coreean)" , TranslateId = 22 },
                new CandidateNationality { NationalityName ="corsicană" , TranslateId = 23 },
                new CandidateNationality { NationalityName ="croată" , TranslateId = 24 },
                new CandidateNationality { NationalityName ="daneză" , TranslateId = 25 },
                new CandidateNationality { NationalityName ="elvețian" , TranslateId = 26 },
                new CandidateNationality { NationalityName ="englez(engleză)" , TranslateId = 27 },
                new CandidateNationality { NationalityName ="estoniană" , TranslateId = 28 },
                new CandidateNationality { NationalityName ="finlandeză" , TranslateId = 29 },
                new CandidateNationality { NationalityName ="franceză(francez)" , TranslateId = 30 },
                new CandidateNationality { NationalityName ="friulană" , TranslateId = 31 },
                new CandidateNationality { NationalityName ="germană" , TranslateId = 32 },
                new CandidateNationality { NationalityName ="ghaneză" , TranslateId = 33 },
                new CandidateNationality { NationalityName ="greacă" , TranslateId = 34 },
                new CandidateNationality { NationalityName ="indian" , TranslateId = 35 },
                new CandidateNationality { NationalityName ="irlandeză" , TranslateId = 36 },
                new CandidateNationality { NationalityName ="italiană" , TranslateId = 37 },
                new CandidateNationality { NationalityName ="japoneză(japonez)" , TranslateId = 38 },
                new CandidateNationality { NationalityName ="latină" , TranslateId = 39 },
                new CandidateNationality { NationalityName ="letonă" , TranslateId = 40 },
                new CandidateNationality { NationalityName ="lituaniană" , TranslateId = 41 },
                new CandidateNationality { NationalityName ="maghiară" , TranslateId = 42 },
                new CandidateNationality { NationalityName ="malgașă" , TranslateId = 43 },
                new CandidateNationality { NationalityName ="malteză" , TranslateId = 44 },
                new CandidateNationality { NationalityName ="moldovean" , TranslateId = 45 },
                new CandidateNationality { NationalityName ="neerlandeză" , TranslateId = 46 },
                new CandidateNationality { NationalityName ="niponă(nipon)" , TranslateId = 47 },
                new CandidateNationality { NationalityName ="norvegiană" , TranslateId = 48 },
                new CandidateNationality { NationalityName ="occitană" , TranslateId = 49 },
                new CandidateNationality { NationalityName ="olandeză" , TranslateId = 50 },
                new CandidateNationality { NationalityName ="pakistanez" , TranslateId = 51 },
                new CandidateNationality { NationalityName ="poloneză" , TranslateId = 52 },
                new CandidateNationality { NationalityName ="portugheză" , TranslateId = 53 },
                new CandidateNationality { NationalityName ="română(român)" , TranslateId = 54 },
                new CandidateNationality { NationalityName ="româncă" , TranslateId = 55 },
                new CandidateNationality { NationalityName ="rusă" , TranslateId = 56 },
                new CandidateNationality { NationalityName ="sârbă" , TranslateId = 57 },
                new CandidateNationality { NationalityName ="scoțiană" , TranslateId = 58 },
                new CandidateNationality { NationalityName ="siciliană" , TranslateId = 59 },
                new CandidateNationality { NationalityName ="slavă" , TranslateId = 60 },
                new CandidateNationality { NationalityName ="slovenă" , TranslateId = 61 },
                new CandidateNationality { NationalityName ="spaniol" , TranslateId = 62 },
                new CandidateNationality { NationalityName ="spaniolă" , TranslateId = 63 },
                new CandidateNationality { NationalityName ="sudaneză" , TranslateId = 64 },
                new CandidateNationality { NationalityName ="suedeză" , TranslateId = 65 },
                new CandidateNationality { NationalityName ="tadjică" , TranslateId = 66 },
                new CandidateNationality { NationalityName ="turcă" , TranslateId = 67 },
                new CandidateNationality { NationalityName ="ungur" , TranslateId = 68 },
                new CandidateNationality { NationalityName ="vietnameză" , TranslateId = 69 },
                new CandidateNationality { NationalityName ="yuki" , TranslateId = 70 },
            };

            var nationalities = appDbContext.CandidateNationalities.ToList();

            foreach (var item in newCandidateNationality)
            {
                var existentKey = nationalities.FirstOrDefault(k => k.NationalityName == item.NationalityName);

                if (existentKey == null)
                {
                    appDbContext.CandidateNationalities.Add(item);
                }
                else
                {
                    nationalities.Remove(existentKey);
                }
            }

            if (nationalities.Any())
            {
                appDbContext.CandidateNationalities.RemoveRange(nationalities);
            }

            appDbContext.SaveChanges();

        }

        private static void AddCandidateCitizenship(AppDbContext appDbContext)
        {
            var newCandidateCitizenship = new List<CandidateCitizenship>()
            {
                new CandidateCitizenship { CitizenshipName ="Afgană(Afgan)" , TranslateId = 1 },
                new CandidateCitizenship { CitizenshipName ="Afgană" , TranslateId = 2 },
                new CandidateCitizenship { CitizenshipName ="Albaneză(Albanez)" , TranslateId = 3 },
                new CandidateCitizenship { CitizenshipName ="Algerian" , TranslateId = 4 },
                new CandidateCitizenship { CitizenshipName ="Americană(American)" , TranslateId = 5 },
                new CandidateCitizenship { CitizenshipName ="Arabă" , TranslateId = 6 },
                new CandidateCitizenship { CitizenshipName ="Armeană" , TranslateId = 7 },
                new CandidateCitizenship { CitizenshipName ="Aromân" , TranslateId = 8 },
                new CandidateCitizenship { CitizenshipName ="Aromână" , TranslateId = 9 },
                new CandidateCitizenship { CitizenshipName ="Austriac" , TranslateId = 10 },
                new CandidateCitizenship { CitizenshipName ="Azer(azeră)" , TranslateId = 11 },
                new CandidateCitizenship { CitizenshipName ="belgiană" , TranslateId = 12 },
                new CandidateCitizenship { CitizenshipName ="britanic" , TranslateId = 13 },
                new CandidateCitizenship { CitizenshipName ="bulgară(bulgar)" , TranslateId = 14 },
                new CandidateCitizenship { CitizenshipName ="calabreză" , TranslateId = 15 },
                new CandidateCitizenship { CitizenshipName ="canadian" , TranslateId = 16 },
                new CandidateCitizenship { CitizenshipName ="catalană(catalan)" , TranslateId = 17 },
                new CandidateCitizenship { CitizenshipName ="cehă(ceh)" , TranslateId = 18 },
                new CandidateCitizenship { CitizenshipName ="cehoaică" , TranslateId = 19 },
                new CandidateCitizenship { CitizenshipName ="chineză(chinez)" , TranslateId = 20 },
                new CandidateCitizenship { CitizenshipName ="chinezoaică" , TranslateId = 21 },
                new CandidateCitizenship { CitizenshipName ="coreeană(coreean)" , TranslateId = 22 },
                new CandidateCitizenship { CitizenshipName ="corsicană" , TranslateId = 23 },
                new CandidateCitizenship { CitizenshipName ="croată" , TranslateId = 24 },
                new CandidateCitizenship { CitizenshipName ="daneză" , TranslateId = 25 },
                new CandidateCitizenship { CitizenshipName ="elvețian" , TranslateId = 26 },
                new CandidateCitizenship { CitizenshipName ="englez(engleză)" , TranslateId = 27 },
                new CandidateCitizenship { CitizenshipName ="estoniană" , TranslateId = 28 },
                new CandidateCitizenship { CitizenshipName ="finlandeză" , TranslateId = 29 },
                new CandidateCitizenship { CitizenshipName ="franceză(francez)" , TranslateId = 30 },
                new CandidateCitizenship { CitizenshipName ="friulană" , TranslateId = 31 },
                new CandidateCitizenship { CitizenshipName ="germană" , TranslateId = 32 },
                new CandidateCitizenship { CitizenshipName ="ghaneză" , TranslateId = 33 },
                new CandidateCitizenship { CitizenshipName ="greacă" , TranslateId = 34 },
                new CandidateCitizenship { CitizenshipName ="indian" , TranslateId = 35 },
                new CandidateCitizenship { CitizenshipName ="irlandeză" , TranslateId = 36 },
                new CandidateCitizenship { CitizenshipName ="italiană" , TranslateId = 37 },
                new CandidateCitizenship { CitizenshipName ="japoneză(japonez)" , TranslateId = 38 },
                new CandidateCitizenship { CitizenshipName ="latină" , TranslateId = 39 },
                new CandidateCitizenship { CitizenshipName ="letonă" , TranslateId = 40 },
                new CandidateCitizenship { CitizenshipName ="lituaniană" , TranslateId = 41 },
                new CandidateCitizenship { CitizenshipName ="maghiară" , TranslateId = 42 },
                new CandidateCitizenship { CitizenshipName ="malgașă" , TranslateId = 43 },
                new CandidateCitizenship { CitizenshipName ="malteză" , TranslateId = 44 },
                new CandidateCitizenship { CitizenshipName ="moldovean" , TranslateId = 45 },
                new CandidateCitizenship { CitizenshipName ="neerlandeză" , TranslateId = 46 },
                new CandidateCitizenship { CitizenshipName ="niponă(nipon)" , TranslateId = 47 },
                new CandidateCitizenship { CitizenshipName ="norvegiană" , TranslateId = 48 },
                new CandidateCitizenship { CitizenshipName ="occitană" , TranslateId = 49 },
                new CandidateCitizenship { CitizenshipName ="olandeză" , TranslateId = 50 },
                new CandidateCitizenship { CitizenshipName ="pakistanez" , TranslateId = 51 },
                new CandidateCitizenship { CitizenshipName ="poloneză" , TranslateId = 52 },
                new CandidateCitizenship { CitizenshipName ="portugheză" , TranslateId = 53 },
                new CandidateCitizenship { CitizenshipName ="română(român)" , TranslateId = 54 },
                new CandidateCitizenship { CitizenshipName ="româncă" , TranslateId = 55 },
                new CandidateCitizenship { CitizenshipName ="rusă" , TranslateId = 56 },
                new CandidateCitizenship { CitizenshipName ="sârbă" , TranslateId = 57 },
                new CandidateCitizenship { CitizenshipName ="scoțiană" , TranslateId = 58 },
                new CandidateCitizenship { CitizenshipName ="siciliană" , TranslateId = 59 },
                new CandidateCitizenship { CitizenshipName ="slavă" , TranslateId = 60 },
                new CandidateCitizenship { CitizenshipName ="slovenă" , TranslateId = 61 },
                new CandidateCitizenship { CitizenshipName ="spaniol" , TranslateId = 62 },
                new CandidateCitizenship { CitizenshipName ="spaniolă" , TranslateId = 63 },
                new CandidateCitizenship { CitizenshipName ="sudaneză" , TranslateId = 64 },
                new CandidateCitizenship { CitizenshipName ="suedeză" , TranslateId = 65 },
                new CandidateCitizenship { CitizenshipName ="tadjică" , TranslateId = 66 },
                new CandidateCitizenship { CitizenshipName ="turcă" , TranslateId = 67 },
                new CandidateCitizenship { CitizenshipName ="ungur" , TranslateId = 68 },
                new CandidateCitizenship { CitizenshipName ="vietnameză" , TranslateId = 69 },
                new CandidateCitizenship { CitizenshipName ="yuki" , TranslateId = 70 },
            };

            var citizenship = appDbContext.CandidateCitizens.ToList();

            foreach (var item in newCandidateCitizenship)
            {
                var existentKey = citizenship.FirstOrDefault(k => k.CitizenshipName == item.CitizenshipName);

                if (existentKey == null)
                {
                    appDbContext.CandidateCitizens.Add(item);
                }
                else
                {
                    citizenship.Remove(existentKey);
                }
            }

            if (citizenship.Any())
            {
                appDbContext.CandidateCitizens.RemoveRange(citizenship);
            }

            appDbContext.SaveChanges();
        }

        private static void AddModernLaguages(AppDbContext appDbContext)
        {
            var newModernLanguage = new List<ModernLanguage>()
            { 
                new ModernLanguage{Name="Chineză", TranslateId = 1 },
                new ModernLanguage{Name="Spaniolă", TranslateId = 2 },
                new ModernLanguage{Name="Engleză", TranslateId = 3 },
                new ModernLanguage{Name="Hindi", TranslateId = 4 },
                new ModernLanguage{Name="Franceză", TranslateId = 5 },
                new ModernLanguage{Name="Arabă", TranslateId = 6 },
                new ModernLanguage{Name="Portugheză", TranslateId = 7 },
                new ModernLanguage{Name="Bengali", TranslateId = 8 },
                new ModernLanguage{Name="Rusă", TranslateId = 9 },
                new ModernLanguage{Name="Japoneză", TranslateId = 10 },
                new ModernLanguage{Name="Italiană", TranslateId = 11 },
            };

            var languages = appDbContext.ModernLanguages.ToList();

            foreach (var item in newModernLanguage)
            {
                var existentKey = languages.FirstOrDefault(k => k.Name == item.Name);

                if (existentKey == null)
                {
                    appDbContext.ModernLanguages.Add(item);
                }
                else
                {
                    languages.Remove(existentKey);
                }
            }

            if (languages.Any())
            {
                appDbContext.ModernLanguages.RemoveRange(languages);
            }

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

        private static void AddSolicitedVacantPositionMessages(AppDbContext appDbContext)
        {
            var existMessages = appDbContext.SolicitedVacantPositionEmailMessages.Any();

            if (!existMessages)
            {
                const string messageToReject = "<p style=\\\"text-align:center;\\\">" +
                                                "<strong>APLICAREA LA O POZITIE VACANTA</strong></p>" +
                                                "<p style=\\\"margin-left:0px;\\\"><strong>Sir / Madam,</strong></p>" +
                                                "<p style=\\\"margin-left:0px;\\\">&nbsp;</p>" +
                                                "<p style=\\\"margin-left:0px;text-align:center;\\\">" +
                                                "<strong>Vă mulțumim pentru aplicarea Cv-ului la concursului de angajare in cadrul pozitiei date, dar cu parere de rau nu sunteti o candidatura potrivita pentru pozitia data</strong></p>" +
                                                "<p style=\\\"margin-left:0px;\\\">&nbsp;</p>" +
                                                "<p style=\\\"margin-left:0px;\\\"><strong>Cu stimă, MAI</strong></p>";

                var rejectMessage = new SolicitedVacantPositionEmailMessage
                {
                    Message = messageToReject,
                    MessageType = SolicitedVacantPositionEmailMessageEnum.Reject
                };

                const string messageToApprove = "<p style=\\\"text-align:center;\\\"><strong>APLICAREA LA O POZITIE VACANTA</strong></p>" +
                                                "<p style=\\\"margin-left:0px;\\\"><strong>Sir / Madam,</strong></p>" +
                                                "<p style=\\\"margin-left:0px;\\\">&nbsp;</p>" +
                                                "<p style=\\\"margin-left:0px;text-align:center;\\\"><strong>Vă mulțumim pentru aplicarea Cv-ului la concursului de angajare in cadrul pozitiei date, privind prezenta tuturor documentelor in regula, sunteti admisi pentru trecerea urmatoarelor probe</strong></p>" +
                                                "<p style=\\\"margin-left:0px;\\\">&nbsp;</p>" +
                                                "<p style=\\\"margin-left:0px;\\\"><strong>Cu stimă, MAI</strong></p>";

                var approvalMessage = new SolicitedVacantPositionEmailMessage
                {
                    Message = messageToApprove,
                    MessageType = SolicitedVacantPositionEmailMessageEnum.Approve
                };

                const string messageToWait = "<p style=\\\"text-align:center;\\\"><strong>APLICAREA LA O POZITIE VACANTA</strong></p>" +
                                             "<p style=\\\"margin-left:0px;\\\"><strong>Sir / Madam,</strong></p>" +
                                             "<p style=\\\"margin-left:0px;\\\">&nbsp;</p>" +
                                             "<p style=\\\"margin-left:0px;text-align:center;\\\"><strong>Vă mulțumim pentru aplicarea Cv-ului la concursului de angajare in cadrul pozitiei date, la moment sunteti pozitionati intr-un statut de asteptare</strong></p>" +
                                             "<p style=\\\"margin-left:0px;text-align:center;\\\"><strong>este necesar sa revizuiti integritatea tuturor documentelor incarcate si intro perioada scurta dosarul dvs. va fi revizuit din nou</strong></p>" +
                                             "<p style=\\\"margin-left:0px;\\\">&nbsp;</p>" +
                                             "<p style=\\\"margin-left:0px;\\\"><strong>Cu stimă, MAI</strong></p>";

                var waitMessage = new SolicitedVacantPositionEmailMessage
                {
                    Message = messageToWait,
                    MessageType = SolicitedVacantPositionEmailMessageEnum.Waiting
                };

                appDbContext.SolicitedVacantPositionEmailMessages.Add(rejectMessage);
                appDbContext.SolicitedVacantPositionEmailMessages.Add(approvalMessage);
                appDbContext.SolicitedVacantPositionEmailMessages.Add(waitMessage);
            }
        }

    }
}
