using System;
using System.Linq;
using RERU.Data.Entities.PersonalEntities.Files;

namespace RERU.Data.Entities.PersonalEntities.StaticExtensions
{
    public static class ContractorExtensions
    {
        public static string GetFullName(this Contractor contractor) => $"{contractor.UserProfile.FirstName} {contractor.UserProfile.LastName} {contractor.UserProfile.FatherName}";

        ///<summary>
        ///in service contractors
        ///</summary>
        public static IQueryable<Contractor> InServiceAt(this IQueryable<Contractor> items, DateTime on)
        {
            return items.Where(c =>
                    c.Positions.Any(p =>
                        (p.FromDate == null && p.ToDate == null)
                        || (p.ToDate == null && p.FromDate != null && p.FromDate < on)
                        || (p.FromDate == null && p.ToDate != null && p.ToDate > on)
                        || (p.FromDate != null && p.ToDate != null && p.FromDate < on && p.ToDate > on)));
        }

        ///<summary>
        ///dismised contractors
        ///</summary>
        public static IQueryable<Contractor> DismissedAt(this IQueryable<Contractor> items, DateTime on)
        {
            return items.Where(c => !c.Positions.Any()
                                        || c.Positions.All(p =>
                          (p.FromDate == null && p.ToDate != null && on > p.ToDate)
                       || (p.FromDate != null && p.ToDate == null && on < p.FromDate)
                       || (p.FromDate != null && p.ToDate != null && (on < p.FromDate || on > p.ToDate))));
        }

        ///<summary>
        ///common search by firstName and/or lastName
        ///</summary>
        public static IQueryable<Contractor> FilterByName(this IQueryable<Contractor> items, string contractorName)
        {
            var toSearch = contractorName.Split(' ').ToList();

            if (toSearch.Count == 1)
            {
                return items.Where(x =>
                    x.UserProfile.FirstName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.LastName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.FatherName.ToLower().Contains(toSearch.First().ToLower()));
            }

            if (toSearch.Count == 2)
            {
                return items.Where(x =>
                    x.UserProfile.FirstName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.LastName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.FirstName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.UserProfile.LastName.ToLower().Contains(toSearch.Last().ToLower()));
            }

            if (toSearch.Count == 3)
            {
                return items.Where(x =>
                    x.UserProfile.FirstName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.LastName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.FatherName.ToLower().Contains(toSearch.First().ToLower())
                    || x.UserProfile.FirstName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.UserProfile.LastName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.UserProfile.FatherName.ToLower().Contains(toSearch.Last().ToLower()));
            }

            return items;
        }

        public static IQueryable<ContractorFile> FilterOrdersByContractorName(this IQueryable<ContractorFile> items, string contractorName)
        {
            var toSearch = contractorName.Split(' ').ToList();

            if (toSearch.Count == 1)
            {
                return items.Where(x =>
                    x.Contractor.FirstName.ToLower().Contains(toSearch.First().ToLower())
                    || x.Contractor.LastName.ToLower().Contains(toSearch.First().ToLower())
                    || x.Contractor.FatherName.ToLower().Contains(toSearch.First().ToLower()));
            }

            if (toSearch.Count == 2)
            {
                return items.Where(x =>
                    x.Contractor.FirstName.ToLower().Contains(toSearch.First().ToLower())
                    || x.Contractor.LastName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.Contractor.FirstName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.Contractor.LastName.ToLower().Contains(toSearch.First().ToLower()));
            }

            if (toSearch.Count == 3)
            {
                return items.Where(x =>
                    x.Contractor.FirstName.ToLower().Contains(toSearch.First().ToLower())
                    || x.Contractor.LastName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.Contractor.FatherName.ToLower().Contains(toSearch.First().ToLower())
                    || x.Contractor.FirstName.ToLower().Contains(toSearch.Last().ToLower())
                    || x.Contractor.LastName.ToLower().Contains(toSearch.First().ToLower())
                    || x.Contractor.FatherName.ToLower().Contains(toSearch.Last().ToLower()));
            }

            return items;
        }
    }

}
