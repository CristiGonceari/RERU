using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Data.Entities.StaticExtensions
{
    public static class UserExtensions
    {
        public static string GetFullName(this UserProfile user) => $"{user.FirstName} {user.LastName}";

        ///<summary>
        ///common search by firstName and/or lastName
        ///</summary>
        public static IQueryable<UserProfile> FilterByName(this IQueryable<UserProfile> items, string userName)
        {
            var toSearch = userName.Split(' ').ToList();

            return toSearch.Count switch
            {
                1 => items.Where(x => x.FirstName.Contains(toSearch.First()) || x.LastName.Contains(toSearch.First())),
                2 => items.Where(x =>
                    x.FirstName.Contains(toSearch.First()) || x.LastName.Contains(toSearch.First()) ||
                    x.FirstName.Contains(toSearch.Last()) || x.LastName.Contains(toSearch.Last())),
                3 => items.Where(x =>
                    x.FirstName.Contains(toSearch.First()) || x.LastName.Contains(toSearch.First()) ||
                    x.FirstName.Contains(toSearch.Last()) || x.LastName.Contains(toSearch.Last())),
                _ => items
            };
        }
    }
}
