using System.Collections.Generic;
using System.Linq;
using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities;
using RERU.Data.Entities.Documents;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.Documents;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;
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
            AddStudyType(appDbContext);
            AddMaterialStatusTypes(appDbContext);
            AddBaseNomenclatureTypes(appDbContext);
        }

        private static void AddBaseDocumentTemplateKeys(AppDbContext appDbContext)
        {
            
            var newValues = new List<DocumentTemplateKey>()
            {
                //testType testtemplate
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_azi}", Description= "Data pentru ziua de azi", FileType = FileTypeEnum.testtemplate, TranslateId = 1 },
                new DocumentTemplateKey { KeyName="{cheia_numelui_șablonului_de_testare}", Description= "Numele șablonului de test", FileType = FileTypeEnum.testtemplate, TranslateId = 22 },
                new DocumentTemplateKey { KeyName="{cheia_numelui_categoriei_șablonului_de_testare}", Description= "Numarul de categorii din șablonul de test", FileType = FileTypeEnum.testtemplate, TranslateId = 23 },
                new DocumentTemplateKey { KeyName="{cheia_regulilor}", Description= "Reguli", FileType = FileTypeEnum.testtemplate, TranslateId = 24 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_total_de_întrebări}", Description= "Numarul de intrebari", FileType = FileTypeEnum.testtemplate, TranslateId = 25 },
                new DocumentTemplateKey { KeyName="{cheie_minim_punctaj}", Description= "Punctajul Minim", FileType = FileTypeEnum.testtemplate, TranslateId = 26 },
                new DocumentTemplateKey { KeyName="{cheie_de_durată}", Description= "Durata", FileType = FileTypeEnum.testtemplate, TranslateId = 27 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_de_maxim_posibile_erori}", Description= "Maxim posibile erori", FileType = FileTypeEnum.testtemplate, TranslateId = 28 },
                new DocumentTemplateKey { KeyName="{cheie_cu_formula_pentru_un_singur_raspuns}", Description= "Formula pentru intrebari cu un raspuns", FileType = FileTypeEnum.testtemplate, TranslateId = 29 },
                new DocumentTemplateKey { KeyName="{cheie_cu_formula_pentru_răspunsuri_multiple}", Description= "Formula pentru intrebari cu respunsuri multiple", FileType = FileTypeEnum.testtemplate, TranslateId = 30 },
                new DocumentTemplateKey { KeyName="{cheie_cu_statutul_șablonului}", Description= "Statutul șablonului de test", FileType = FileTypeEnum.testtemplate, TranslateId = 31 },
                new DocumentTemplateKey { KeyName="{cheie_modul_șablonului}", Description= "Modul șablonului de test", FileType = FileTypeEnum.testtemplate, TranslateId = 32 },
                new DocumentTemplateKey { KeyName="{cheie_cu_ordinea_întrebărilor_in_test}", Description= "Ordinea întrebărilor in test", FileType = FileTypeEnum.testtemplate, TranslateId = 33 },

                //testType test
                new DocumentTemplateKey { KeyName="{cheie_pentru_data_de_azi}", Description= "Data pentru ziua de azi", FileType = FileTypeEnum.test, TranslateId = 34 },
                new DocumentTemplateKey { KeyName="{cheie_cu_punctaj_acumulat}", Description= "Punctaj Acumulat", FileType = FileTypeEnum.test, TranslateId = 35 },
                new DocumentTemplateKey { KeyName="{cheie_cu_statutul_testului}", Description= "Statutul testului", FileType = FileTypeEnum.test, TranslateId = 37 },
                new DocumentTemplateKey { KeyName="{cheie_cu_rezultatul_testului}", Description= "Rezultatul testului", FileType = FileTypeEnum.test, TranslateId = 38 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_programata_a_testului}", Description= "Data programata pentru inceperea testului", FileType = FileTypeEnum.test, TranslateId = 21 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_inceput_a_testului}", Description= "Data cand sa inceput testului", FileType = FileTypeEnum.test, TranslateId = 20 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evenimentului}", Description= "Numele evenimentul la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 9 },
                new DocumentTemplateKey { KeyName="{cheie_cu_descrierea_evenimentului}", Description= "Detalii despre evenimentul la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 2 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_inceput_a_evenimentului}", Description= "Data de incepere a evenimentul la care este atasat testul", FileType = FileTypeEnum.test,  TranslateId = 3 },
                new DocumentTemplateKey { KeyName="{cheie_cu_data_de_incheiere_a_evenimentului}", Description= "Data de incheiere a evenimentul la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 4 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_locatiei}", Description= "Numele locatiei la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 5 },
                new DocumentTemplateKey { KeyName="{cheie_cu_descrierea_locatiei}", Description= "Detalii despre locatia la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 6 },
                new DocumentTemplateKey { KeyName="{cheie_cu_adresa_locatiei}", Description= "Adresa locatiei la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 7 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numarul_de_locuri_ale_locatiei}", Description= "Numarul de locuri ale locatiei la care este atasat testul", FileType = FileTypeEnum.test, TranslateId = 10 },
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_testului}", Description= "Numele testului", FileType = FileTypeEnum.test, TranslateId = 41 },

                        //Evaluated person
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evaluatului}", Description= "Numele evaluatului", FileType = FileTypeEnum.test, TranslateId = 18 },
                new DocumentTemplateKey { KeyName="{cheie_cu_prenumele_evaluatului}", Description= "Prenumele evaluatului", FileType = FileTypeEnum.test, TranslateId = 11 },
                new DocumentTemplateKey { KeyName="{cheie_cu_patronimicul_evaluatului}", Description= "Patronimicul evaluatului", FileType = FileTypeEnum.test, TranslateId = 12 },
                new DocumentTemplateKey { KeyName="{cheie_cu_IDNP_evaluatului}", Description= "IDNP din buletinul evaluatului", FileType = FileTypeEnum.test, TranslateId = 13 },
                new DocumentTemplateKey { KeyName="{cheie_cu_email_evaluatului}", Description= "E-mailul evaluatului", FileType = FileTypeEnum.test, TranslateId = 14 },
                        //Evaluator
                new DocumentTemplateKey { KeyName="{cheie_cu_numele_evaluatorului}", Description= "Numele evaluatorului", FileType = FileTypeEnum.test, TranslateId = 15 },
                new DocumentTemplateKey { KeyName="{cheie_cu_prenumele_evaluatorului}", Description= "Prenumele evaluatorului", FileType = FileTypeEnum.test, TranslateId = 16 },
                new DocumentTemplateKey { KeyName="{cheie_cu_patronimicul_evaluatorului}", Description= "Patronimicul evaluatorului", FileType = FileTypeEnum.test, TranslateId = 17 },
                new DocumentTemplateKey { KeyName="{cheie_cu_IDNP_evaluatorului}", Description= "IDNP din buletinul evaluatorului", FileType = FileTypeEnum.test, TranslateId = 39 },
                new DocumentTemplateKey { KeyName="{cheie_cu_email_evaluatorului}", Description= "E-mailul evaluatorului", FileType = FileTypeEnum.test, TranslateId = 40 },
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
                new CandidateNationality { NationalityName ="Belgiană" , TranslateId = 12 },
                new CandidateNationality { NationalityName ="Britanic" , TranslateId = 13 },
                new CandidateNationality { NationalityName ="Bulgară(bulgar)" , TranslateId = 14 },
                new CandidateNationality { NationalityName ="Calabreză" , TranslateId = 15 },
                new CandidateNationality { NationalityName ="Canadian" , TranslateId = 16 },
                new CandidateNationality { NationalityName ="Catalană(catalan)" , TranslateId = 17 },
                new CandidateNationality { NationalityName ="Cehă(ceh)" , TranslateId = 18 },
                new CandidateNationality { NationalityName ="Cehoaică" , TranslateId = 19 },
                new CandidateNationality { NationalityName ="Chineză(chinez)" , TranslateId = 20 },
                new CandidateNationality { NationalityName ="Chinezoaică" , TranslateId = 21 },
                new CandidateNationality { NationalityName ="Coreeană(coreean)" , TranslateId = 22 },
                new CandidateNationality { NationalityName ="Corsicană" , TranslateId = 23 },
                new CandidateNationality { NationalityName ="Croată" , TranslateId = 24 },
                new CandidateNationality { NationalityName ="Daneză" , TranslateId = 25 },
                new CandidateNationality { NationalityName ="Elvețian" , TranslateId = 26 },
                new CandidateNationality { NationalityName ="Englez(engleză)" , TranslateId = 27 },
                new CandidateNationality { NationalityName ="Estoniană" , TranslateId = 28 },
                new CandidateNationality { NationalityName ="Finlandeză" , TranslateId = 29 },
                new CandidateNationality { NationalityName ="Franceză(francez)" , TranslateId = 30 },
                new CandidateNationality { NationalityName ="Friulană" , TranslateId = 31 },
                new CandidateNationality { NationalityName ="Germană" , TranslateId = 32 },
                new CandidateNationality { NationalityName ="Ghaneză" , TranslateId = 33 },
                new CandidateNationality { NationalityName ="Greacă" , TranslateId = 34 },
                new CandidateNationality { NationalityName ="Indian" , TranslateId = 35 },
                new CandidateNationality { NationalityName ="Irlandeză" , TranslateId = 36 },
                new CandidateNationality { NationalityName ="Italiană" , TranslateId = 37 },
                new CandidateNationality { NationalityName ="Japoneză(japonez)" , TranslateId = 38 },
                new CandidateNationality { NationalityName ="Latină" , TranslateId = 39 },
                new CandidateNationality { NationalityName ="Letonă" , TranslateId = 40 },
                new CandidateNationality { NationalityName ="Lituaniană" , TranslateId = 41 },
                new CandidateNationality { NationalityName ="Maghiară" , TranslateId = 42 },
                new CandidateNationality { NationalityName ="Malgașă" , TranslateId = 43 },
                new CandidateNationality { NationalityName ="Malteză" , TranslateId = 44 },
                new CandidateNationality { NationalityName ="Moldovean" , TranslateId = 45 },
                new CandidateNationality { NationalityName ="Neerlandeză" , TranslateId = 46 },
                new CandidateNationality { NationalityName ="Niponă(nipon)" , TranslateId = 47 },
                new CandidateNationality { NationalityName ="Norvegiană" , TranslateId = 48 },
                new CandidateNationality { NationalityName ="Occitană" , TranslateId = 49 },
                new CandidateNationality { NationalityName ="Olandeză" , TranslateId = 50 },
                new CandidateNationality { NationalityName ="Pakistanez" , TranslateId = 51 },
                new CandidateNationality { NationalityName ="Poloneză" , TranslateId = 52 },
                new CandidateNationality { NationalityName ="Portugheză" , TranslateId = 53 },
                new CandidateNationality { NationalityName ="Română(român)" , TranslateId = 54 },
                new CandidateNationality { NationalityName ="Româncă" , TranslateId = 55 },
                new CandidateNationality { NationalityName ="Rusă" , TranslateId = 56 },
                new CandidateNationality { NationalityName ="sârbă" , TranslateId = 57 },
                new CandidateNationality { NationalityName ="Scoțiană" , TranslateId = 58 },
                new CandidateNationality { NationalityName ="Siciliană" , TranslateId = 59 },
                new CandidateNationality { NationalityName ="Slavă" , TranslateId = 60 },
                new CandidateNationality { NationalityName ="Slovenă" , TranslateId = 61 },
                new CandidateNationality { NationalityName ="Spaniol" , TranslateId = 62 },
                new CandidateNationality { NationalityName ="Spaniolă" , TranslateId = 63 },
                new CandidateNationality { NationalityName ="Sudaneză" , TranslateId = 64 },
                new CandidateNationality { NationalityName ="Suedeză" , TranslateId = 65 },
                new CandidateNationality { NationalityName ="Tadjică" , TranslateId = 66 },
                new CandidateNationality { NationalityName ="Turcă" , TranslateId = 67 },
                new CandidateNationality { NationalityName ="Ungur" , TranslateId = 68 },
                new CandidateNationality { NationalityName ="Vietnameză" , TranslateId = 69 },
                new CandidateNationality { NationalityName ="Yuki" , TranslateId = 70 },
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
                new CandidateCitizenship { CitizenshipName ="Belgiană" , TranslateId = 12 },
                new CandidateCitizenship { CitizenshipName ="Britanic" , TranslateId = 13 },
                new CandidateCitizenship { CitizenshipName ="Bulgară(bulgar)" , TranslateId = 14 },
                new CandidateCitizenship { CitizenshipName ="Calabreză" , TranslateId = 15 },
                new CandidateCitizenship { CitizenshipName ="Canadian" , TranslateId = 16 },
                new CandidateCitizenship { CitizenshipName ="Catalană(catalan)" , TranslateId = 17 },
                new CandidateCitizenship { CitizenshipName ="Cehă(ceh)" , TranslateId = 18 },
                new CandidateCitizenship { CitizenshipName ="Cehoaică" , TranslateId = 19 },
                new CandidateCitizenship { CitizenshipName ="Chineză(chinez)" , TranslateId = 20 },
                new CandidateCitizenship { CitizenshipName ="Chinezoaică" , TranslateId = 21 },
                new CandidateCitizenship { CitizenshipName ="Coreeană(coreean)" , TranslateId = 22 },
                new CandidateCitizenship { CitizenshipName ="Corsicană" , TranslateId = 23 },
                new CandidateCitizenship { CitizenshipName ="Croată" , TranslateId = 24 },
                new CandidateCitizenship { CitizenshipName ="Daneză" , TranslateId = 25 },
                new CandidateCitizenship { CitizenshipName ="Elvețian" , TranslateId = 26 },
                new CandidateCitizenship { CitizenshipName ="Englez(engleză)" , TranslateId = 27 },
                new CandidateCitizenship { CitizenshipName ="Estoniană" , TranslateId = 28 },
                new CandidateCitizenship { CitizenshipName ="Finlandeză" , TranslateId = 29 },
                new CandidateCitizenship { CitizenshipName ="Franceză(francez)" , TranslateId = 30 },
                new CandidateCitizenship { CitizenshipName ="Friulană" , TranslateId = 31 },
                new CandidateCitizenship { CitizenshipName ="Germană" , TranslateId = 32 },
                new CandidateCitizenship { CitizenshipName ="Ghaneză" , TranslateId = 33 },
                new CandidateCitizenship { CitizenshipName ="Greacă" , TranslateId = 34 },
                new CandidateCitizenship { CitizenshipName ="Indian" , TranslateId = 35 },
                new CandidateCitizenship { CitizenshipName ="Irlandeză" , TranslateId = 36 },
                new CandidateCitizenship { CitizenshipName ="Italiană" , TranslateId = 37 },
                new CandidateCitizenship { CitizenshipName ="Japoneză(japonez)" , TranslateId = 38 },
                new CandidateCitizenship { CitizenshipName ="Latină" , TranslateId = 39 },
                new CandidateCitizenship { CitizenshipName ="Letonă" , TranslateId = 40 },
                new CandidateCitizenship { CitizenshipName ="Lituaniană" , TranslateId = 41 },
                new CandidateCitizenship { CitizenshipName ="Maghiară" , TranslateId = 42 },
                new CandidateCitizenship { CitizenshipName ="Malgașă" , TranslateId = 43 },
                new CandidateCitizenship { CitizenshipName ="Malteză" , TranslateId = 44 },
                new CandidateCitizenship { CitizenshipName ="Moldovean" , TranslateId = 45 },
                new CandidateCitizenship { CitizenshipName ="Neerlandeză" , TranslateId = 46 },
                new CandidateCitizenship { CitizenshipName ="Niponă(nipon)" , TranslateId = 47 },
                new CandidateCitizenship { CitizenshipName ="Norvegiană" , TranslateId = 48 },
                new CandidateCitizenship { CitizenshipName ="Pccitană" , TranslateId = 49 },
                new CandidateCitizenship { CitizenshipName ="Plandeză" , TranslateId = 50 },
                new CandidateCitizenship { CitizenshipName ="Pakistanez" , TranslateId = 51 },
                new CandidateCitizenship { CitizenshipName ="Poloneză" , TranslateId = 52 },
                new CandidateCitizenship { CitizenshipName ="Portugheză" , TranslateId = 53 },
                new CandidateCitizenship { CitizenshipName ="Română(român)" , TranslateId = 54 },
                new CandidateCitizenship { CitizenshipName ="Româncă" , TranslateId = 55 },
                new CandidateCitizenship { CitizenshipName ="Rusă" , TranslateId = 56 },
                new CandidateCitizenship { CitizenshipName ="Sârbă" , TranslateId = 57 },
                new CandidateCitizenship { CitizenshipName ="Scoțiană" , TranslateId = 58 },
                new CandidateCitizenship { CitizenshipName ="Siciliană" , TranslateId = 59 },
                new CandidateCitizenship { CitizenshipName ="Slavă" , TranslateId = 60 },
                new CandidateCitizenship { CitizenshipName ="Slovenă" , TranslateId = 61 },
                new CandidateCitizenship { CitizenshipName ="Spaniol" , TranslateId = 62 },
                new CandidateCitizenship { CitizenshipName ="Spaniolă" , TranslateId = 63 },
                new CandidateCitizenship { CitizenshipName ="Sudaneză" , TranslateId = 64 },
                new CandidateCitizenship { CitizenshipName ="Suedeză" , TranslateId = 65 },
                new CandidateCitizenship { CitizenshipName ="Tadjică" , TranslateId = 66 },
                new CandidateCitizenship { CitizenshipName ="Turcă" , TranslateId = 67 },
                new CandidateCitizenship { CitizenshipName ="Ungur" , TranslateId = 68 },
                new CandidateCitizenship { CitizenshipName ="Vietnameză" , TranslateId = 69 },
                new CandidateCitizenship { CitizenshipName ="Yuki" , TranslateId = 70 },
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
                new ModernLanguage{Name="Azerbageana", TranslateId = 10 },
                new ModernLanguage{Name="Armeana", TranslateId = 11 },
                new ModernLanguage{Name="Albaneza", TranslateId = 12 },
                new ModernLanguage{Name="Belorusa", TranslateId = 13 },
                new ModernLanguage{Name="Maghiara", TranslateId = 14 },
                new ModernLanguage{Name="Greaca", TranslateId = 15 },
                new ModernLanguage{Name="Georgiana", TranslateId = 16 },
                new ModernLanguage{Name="Ebraica", TranslateId = 17 },
                new ModernLanguage{Name="Indoneziana", TranslateId = 18 },
                new ModernLanguage{Name="Irlandeza", TranslateId = 19 },
                new ModernLanguage{Name="Islandeza", TranslateId = 20 },
                new ModernLanguage{Name="Italiana", TranslateId = 22 },
                new ModernLanguage{Name="Kazaka", TranslateId = 23 },
                new ModernLanguage{Name="Letona", TranslateId = 24 },
                new ModernLanguage{Name="Lituaniana", TranslateId = 25 },
                new ModernLanguage{Name="Luxemburgheza", TranslateId = 26 },
                new ModernLanguage{Name="Macedoniana", TranslateId = 27 },
                new ModernLanguage{Name="Deutsch", TranslateId = 28 },
                new ModernLanguage{Name="Olandeza", TranslateId = 29 },
                new ModernLanguage{Name="Norvegiana", TranslateId = 30 },
                new ModernLanguage{Name="Slovaca", TranslateId = 31 },
                new ModernLanguage{Name="Slovena", TranslateId = 32 },
                new ModernLanguage{Name="Sudaneza", TranslateId = 33 },
                new ModernLanguage{Name="Thailandeza", TranslateId = 34 },
                new ModernLanguage{Name="Tatara", TranslateId = 35 },
                new ModernLanguage{Name="Turca", TranslateId = 36 },
                new ModernLanguage{Name="Uzbeca", TranslateId = 37 },
                new ModernLanguage{Name="Ucraineana", TranslateId = 38 },
                new ModernLanguage{Name="Finlandeza", TranslateId = 39 },
                new ModernLanguage{Name="Suedeza", TranslateId = 40 },
                new ModernLanguage{Name="Estona", TranslateId = 41 },
                new ModernLanguage{Name="Japoneza", TranslateId = 42 },
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

        private static void AddMaterialStatusTypes(AppDbContext appDbContext)
        {
            var materialStatusTypes = new List<MaterialStatusType>()
            {
                new MaterialStatusType{Name="Celibatar", TranslateId = 1 },
                new MaterialStatusType{Name="Casatorit", TranslateId = 2 },
                new MaterialStatusType{Name="Recasatorit", TranslateId = 3 },
            };

            var types = appDbContext.MaterialStatusTypes.ToList();

            foreach (var item in materialStatusTypes)
            {
                var existentKey = types.FirstOrDefault(k => k.Name == item.Name);

                if (existentKey == null)
                {
                    appDbContext.MaterialStatusTypes.Add(item);
                }
                else
                {
                    types.Remove(existentKey);
                }
            }

            if (types.Any())
            {
                appDbContext.MaterialStatusTypes.RemoveRange(types);
            }

            appDbContext.SaveChanges();
        }

        private static void AddStudyType(AppDbContext appDbContext)
        {
            var newStudyTypes = new List<StudyType>()
            {
                new StudyType { Name = "Studii de Baza", TranslateId = 1 },
                new StudyType { Name = "Universitare", TranslateId = 2 },
                new StudyType { Name = "PostUniversitare", TranslateId = 3 },
                new StudyType { Name = "Cursuri de Perfectionare", TranslateId = 4 },
                new StudyType { Name = "Cursuri de Specializare", TranslateId = 5 },
            };

            var types = appDbContext.StudyTypes.ToList();

            foreach (var item in newStudyTypes)
            {
                var existentKey = types.FirstOrDefault(k => k.Name == item.Name);

                if (existentKey == null)
                {
                    appDbContext.StudyTypes.Add(item);
                }
                else
                {
                    types.Remove(existentKey);
                }
            }

            if (types.Any())
            {
                appDbContext.StudyTypes.RemoveRange(types);
            }

            appDbContext.SaveChanges();

        }

        private static void AddSolicitedVacantPositionMessages(AppDbContext appDbContext)
        {
            var oldObjectMessageToReject = appDbContext.SolicitedVacantPositionEmailMessages.FirstOrDefault(x => x.MessageType == SolicitedVacantPositionEmailMessageEnum.Reject);
            var newMessageToReject = "<p style=\\\"text-align:center;\\\">" +
                                             "<span style=\\\"color:black;\\\">" +
                                             "<i>Dl/Dna {user_name_key}, vă mulțumim pentru depunerea actelor la funcția vacantă solicitată!</i></span>" +
                                             "</p><p style=\\\"text-align:center;\\\"><span style=\\\"color:black;\\\">" +
                                             "<i>Ne pare rău, nu sunteți eligibil/ă pentru testele de evaluare.</i></span></p><p style=\\\"text-align:center;\\\"><span style=\\\"color:black;\\\">" +
                                             "<i>Cu respect MAI.</i></span></p>";

            var oldObjectMessageToApprove = appDbContext.SolicitedVacantPositionEmailMessages.FirstOrDefault(x => x.MessageType == SolicitedVacantPositionEmailMessageEnum.Approve);
            var newMessageToApprove = "<p style=\\\"text-align:center;\\\">" +
                                              "<span style=\\\"color:black;\\\">" +
                                              "<i>Dl/Dna {user_name_key}, vă mulțumim pentru depunerea actelor la funcția vacantă solicitată!</i>" +
                                              "</span></p><p style=\\\"text-align:center;\\\">" +
                                              "<span style=\\\"color:black;\\\"><i>Sunteți admis/ă la probele de evaluare.</i></span></p>" +
                                              "<p style=\\\"text-align:center;\\\">" +
                                              "<span style=\\\"color:black;\\\"><i>În următoarele zile veți primi notificări pe adresa electronică cu privire la data/ora/locația și modul de desfășurare a acestora.</i></span></p>";

            var oldObjectMessageToWait = appDbContext.SolicitedVacantPositionEmailMessages.FirstOrDefault(x => x.MessageType == SolicitedVacantPositionEmailMessageEnum.Waiting);
            var newMessageToWait = "<p style=\\\"text-align:center;\\\"><span style=\\\"color:black;\\\">" +
                                            "<i>Dl/Dna {user_name_key}, vă mulțumim pentru depunerea actelor la funcția vacantă solicitată!</i></span></p>" +
                                            "<p style=\\\"text-align:center;\\\"><span style=\\\"color:black;\\\">" +
                                            "<i>Sunteți asignat/ă cu statut de asteptare.</i></span></p><p style=\\\"text-align:center;\\\"><span style=\\\"color:black;\\\">" +
                                            "<i>Vă rugăm să examinați documentele necesare atașate postului vacant.</i></span></p>";

            if (oldObjectMessageToReject is null)
            {
                var rejectMessage = new SolicitedVacantPositionEmailMessage
                {
                    Message = newMessageToReject,
                    MessageType = SolicitedVacantPositionEmailMessageEnum.Reject
                };
                
                appDbContext.SolicitedVacantPositionEmailMessages.Add(rejectMessage);
            }
            if (oldObjectMessageToApprove is null)
            {
                var approveMessage = new SolicitedVacantPositionEmailMessage
                {
                    Message = newMessageToApprove,
                    MessageType = SolicitedVacantPositionEmailMessageEnum.Approve
                };

                appDbContext.SolicitedVacantPositionEmailMessages.Add(approveMessage);
            }
            if (oldObjectMessageToWait is null)
            {
                var waitMessage = new SolicitedVacantPositionEmailMessage
                {
                    Message = newMessageToWait,
                    MessageType = SolicitedVacantPositionEmailMessageEnum.Waiting
                };

                appDbContext.SolicitedVacantPositionEmailMessages.Add(waitMessage);
            }

            if (oldObjectMessageToReject != null && oldObjectMessageToReject.Message != newMessageToReject) oldObjectMessageToReject.Message = newMessageToReject;
            if (oldObjectMessageToApprove != null && oldObjectMessageToApprove.Message != newMessageToApprove) oldObjectMessageToApprove.Message = newMessageToApprove;
            if (oldObjectMessageToWait != null && oldObjectMessageToWait.Message != newMessageToWait) oldObjectMessageToWait.Message = newMessageToWait;

            appDbContext.SaveChanges();
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

            if (!appDbContext.HrDocumentTemplateCategories.Any(dc => dc.Id >= 1))
            {
                appDbContext.HrDocumentTemplateCategories.AddRange(new List<HrDocumentTemplateCategory>()
                {
                     new HrDocumentTemplateCategory
                    {
                            Id = 1,
                            Name = "Angajat",
                    },
                     new HrDocumentTemplateCategory
                    {
                            Id = 2,
                            Name = "Companie",
                     }
                }
                );
            }

            if (!appDbContext.DocumentTemplateKeys.Any(dc => dc.Id >= 1))
            {
                appDbContext.HrDocumentTemplateKeys.AddRange(new List<HrDocumentTemplateKey>()
                    {
                        new HrDocumentTemplateKey {Id = 1, KeyName = "{today_date_key}", Description = "Data pentru ziua de azi", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 2, KeyName = "{c_name_key}", Description = "Numele Angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 3, KeyName = "{c_last_name_key}", Description = "Prenumele Angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 4, KeyName = "{c_father_name_key}", Description = "Patronimicul Angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 5, KeyName = "{c_idnp_key}", Description = "IDNP din buletin", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 6, KeyName = "{c_bulletin_series_key}", Description = "Seria buletinului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 7, KeyName = "{c_bulletin_release_by_key}", Description = "Buletinul angajatului a fost emis de", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 8, KeyName = "{c_bulletin_release_day_key}", Description = "Data emiterii buletinului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 9, KeyName = "{c_birthday_key}", Description = "Ziua de nastere a angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 10, KeyName = "{c_work_place_key}", Description = "Locul de munca a angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 11, KeyName = "{c_employment_date_key}", Description = "Ziua angajarii", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 12, KeyName = "{c_work_hours_key}", Description = "Numarul ore de munca pe zi conform contractului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 13, KeyName = "{c_salary_key}", Description = "Salariu angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 14, KeyName = "{c_sex_type_key}", Description = "Sexul angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 15, KeyName = "{c_role_key}", Description = "Rolul angajatului in companie", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 16, KeyName = "{c_dissmisal_date_key}", Description = "Data demisionarii angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 17, KeyName = "{c_internship_days_key}", Description = "Zilele de stagiere a angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 18, KeyName = "{c_address_key}", Description = "Adresa de locuinta a angajatului", HrDocumentCategoriesId= 1},
                        new HrDocumentTemplateKey {Id = 19, KeyName = "{company_key}", Description = "Numele companiei", HrDocumentCategoriesId= 2} ,
                        new HrDocumentTemplateKey {Id = 20, KeyName = "{company_post_code_key}", Description = "Codul postal al companiei", HrDocumentCategoriesId= 2},
                        new HrDocumentTemplateKey {Id = 21, KeyName = "{company_city_key}", Description = "Orasul de locatiune al compamiei", HrDocumentCategoriesId= 2},
                        new HrDocumentTemplateKey {Id = 22, KeyName = "{company_street_key}", Description = "Adresa de locatie a companiei", HrDocumentCategoriesId= 2},
                        new HrDocumentTemplateKey {Id = 23, KeyName = "{company_idno_key}", Description = "IDNO-ul companiei", HrDocumentCategoriesId= 2},
                        new HrDocumentTemplateKey {Id = 24, KeyName = "{director_name_key}", Description = "Numele directorului", HrDocumentCategoriesId= 2},
                        new HrDocumentTemplateKey {Id = 25, KeyName = "{director_last_name_key}", Description = "Prenumele directorului", HrDocumentCategoriesId= 2},
                        new HrDocumentTemplateKey {Id = 26, KeyName = "{minister_srl_key}", Description = "Tipul companiei", HrDocumentCategoriesId= 2},
                    }
                 );
            };

            appDbContext.SaveChanges();
        }
    }
}
