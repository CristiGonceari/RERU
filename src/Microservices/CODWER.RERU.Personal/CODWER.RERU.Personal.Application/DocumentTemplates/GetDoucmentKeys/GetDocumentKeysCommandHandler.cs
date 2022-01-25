using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDoucmentKeys
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Key> Keys { get; set; }
    }

    public class Key
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }

    }
    
    public class GetDocumentKeysCommandHandler :IRequestHandler<GetDocumentCommandKeys, List<Category>>
    {
        public async Task<List<Category>> Handle(GetDocumentCommandKeys request, CancellationToken cancellationToken)
        {
            var listOfCategories = new List<Category>()
            {
                new Category
                {
                    Id = 1,
                    Name = "Angajat", 
                    Keys = new List<Key> {
                       new Key {  Id = 2, KeyName = "{today_date_key}", Description = "Data pentru ziua de azi"},
                       new Key {  Id = 3, KeyName = "{c_nr_order_key}", Description = "Numarul ordinului curent"},
                       new Key {  Id = 4, KeyName = "{c_nr_request_key}", Description = "Numarul pentru cererea curenta"},
                       new Key {  Id = 5, KeyName = "{c_name_key}", Description = "Numele Angajatului"},
                       new Key {  Id = 6, KeyName = "{c_last_name_key}", Description = "Prenumele Angajatului"},
                       new Key {  Id = 7, KeyName = "{c_father_name_key}", Description = "Patronimicul Angajatului"},
                       new Key {  Id = 8, KeyName = "{c_idnp_key}", Description = "IDNP din buletin"},
                       new Key {  Id = 9, KeyName = "{c_bulletin_series_key}", Description = "Seria buletinului"},
                       new Key {  Id = 10, KeyName = "{c_bulletin_release_by_key}", Description = "Buletinul angajatului a fost emis de"},
                       new Key {  Id = 11, KeyName = "{c_bulletin_release_day_key}", Description = "Data emiterii buletinului"},
                       new Key {  Id = 12, KeyName = "{c_birthday_key}", Description = "Ziua de nastere a angajatului"},
                       new Key {  Id = 13, KeyName = "{c_work_place_key}", Description = "Locul de munca a angajatului"},
                       new Key {  Id = 14, KeyName = "{c_employment_date_key}", Description = "Ziua angajarii"},
                       new Key {  Id = 15, KeyName = "{c_work_hours_key}", Description = "Numarul ore de munca pe zi conform contractului"},
                       new Key {  Id = 16, KeyName = "{c_salary_key}", Description = "Salariu angajatului"},
                       new Key {  Id = 17, KeyName = "{c_sex_type_key}", Description = "Sexul angajatului"},
                       new Key {  Id = 18, KeyName = "{c_role_key}", Description = "Rolul angajatului in companie"},
                       new Key {  Id = 19, KeyName = "{c_dissmisal_date_key}", Description = "Data demisionarii angajatului"},
                       new Key {  Id = 20, KeyName = "{c_internship_days_key}", Description = "Zilele de stagiere a angajatului"},
                       new Key {  Id = 21, KeyName = "{c_address_key}", Description = "Adresa de locuinta a angajatului"},
                       new Key {  Id = 22, KeyName = "{_vacation_unnused_key}", Description = "Zilele de vacanta nefolosite calculate per angajat"},
                    },
                    
                },
                 new Category
                {
                    Id = 2,
                    Name = "Concedii",
                    Keys = new List<Key> {
                       new Key {  Id = 23, KeyName = "{vacation_type_key}", Description = "Tipul concediului"},
                       new Key {  Id = 24, KeyName = "{vacation_days_count}", Description = "Totalul de zile disponibile de concediului"},
                       new Key {  Id = 25, KeyName = "{vacation_from_key}", Description = "Inceputul concediului"},
                       new Key {  Id = 26, KeyName = "{vacation_to_key}", Description = "Sfirsitul concediului"},
                    },
                },
                  new Category
                {
                    Id = 3,
                    Name = "Companie",
                    Keys = new List<Key> {
                       new Key {  Id = 27, KeyName = "company_key", Description = "Numele companiei" } ,
                       new Key {  Id = 28, KeyName = "{company_post_code_key}", Description = "Codul postal al companiei"},
                       new Key {  Id = 29, KeyName = "{company_city_key}", Description = "Orasul de locatiune al compamiei"},
                       new Key {  Id = 30, KeyName = "{company_street_key}", Description = "Adresa de locatie a companiei"},
                       new Key {  Id = 31, KeyName = "{company_idno_key}", Description = "IDNO-ul companiei"},
                       new Key {  Id = 32, KeyName = "{director_name_key}", Description = "Numele directorului"},
                       new Key {  Id = 33, KeyName = "{director_last_name_key}", Description = "Prenumele directorului"},
                       new Key {  Id = 34, KeyName = "{minister_srl_key}", Description = "Tipul companiei"},
                    },
                }
            };

            return listOfCategories;
        }
    }
    
}
