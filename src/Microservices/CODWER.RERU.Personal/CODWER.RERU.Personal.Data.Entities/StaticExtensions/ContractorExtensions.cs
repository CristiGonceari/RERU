using CODWER.RERU.Personal.Data.Entities.Files;
using System;
using System.Linq;

namespace CODWER.RERU.Personal.Data.Entities.StaticExtensions
{
    public static class ContractorExtensions
    {
        public static string GetFullName(this Contractor contractor) => $"{contractor.FirstName} {contractor.LastName} {contractor.FatherName}";

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
                    x.FirstName.Contains(toSearch.First())
                    || x.LastName.Contains(toSearch.First())
                    || x.FatherName.Contains(toSearch.First()));
            }

            if (toSearch.Count == 2)
            {
                return items.Where(x =>
                    x.FirstName.Contains(toSearch.First())
                    || x.LastName.Contains(toSearch.First())
                    || x.FirstName.Contains(toSearch.Last())
                    || x.LastName.Contains(toSearch.Last()));
            }

            if (toSearch.Count == 3)
            {
                return items.Where(x =>
                    x.FirstName.Contains(toSearch.First())
                    || x.LastName.Contains(toSearch.First())
                    || x.FatherName.Contains(toSearch.First())
                    || x.FirstName.Contains(toSearch.Last())
                    || x.LastName.Contains(toSearch.Last())
                    || x.FatherName.Contains(toSearch.Last()));
            }

            return items;
        }

        public static IQueryable<ContractorFile> FilterOrdersByContractorName(this IQueryable<ContractorFile> items, string contractorName)
        {
            var toSearch = contractorName.Split(' ').ToList();

            if (toSearch.Count == 1)
            {
                return items.Where(x =>
                    x.Contractor.FirstName.Contains(toSearch.First())
                    || x.Contractor.LastName.Contains(toSearch.First())
                    || x.Contractor.FatherName.Contains(toSearch.First()));
            }

            if (toSearch.Count == 2)
            {
                return items.Where(x =>
                    x.Contractor.FirstName.Contains(toSearch.First())
                    || x.Contractor.LastName.Contains(toSearch.Last())
                    || x.Contractor.FirstName.Contains(toSearch.Last())
                    || x.Contractor.LastName.Contains(toSearch.First()));
            }

            if (toSearch.Count == 3)
            {
                return items.Where(x =>
                    x.Contractor.FirstName.Contains(toSearch.First())
                    || x.Contractor.LastName.Contains(toSearch.Last())
                    || x.Contractor.FatherName.Contains(toSearch.First())
                    || x.Contractor.FirstName.Contains(toSearch.Last())
                    || x.Contractor.LastName.Contains(toSearch.First())
                    || x.Contractor.FatherName.Contains(toSearch.Last()));
            }

            return items;
        }
    }

}
