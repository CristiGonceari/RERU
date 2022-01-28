using System.Linq;

namespace CODWER.RERU.Evaluation.Data.Entities.StaticExtensions
{
    public static class UserExtensions
    {
        public static string GetFullName(this UserProfile user) => $"{user.FirstName} {user.LastName}";

        ///<summary>
        ///common search by firstName and/or lastName and/or patronymic and/or idnp
        ///</summary>
        public static IQueryable<UserProfile> FilterByNameAndIdnp(this IQueryable<UserProfile> items, string userName)
        {
            var toSearch = userName.Split(' ').ToList();

            foreach (var item in toSearch)
            {
                items = items.Where(x =>
                    x.FirstName.Contains(item)
                    || x.LastName.Contains(item)
                    || x.Patronymic.Contains(item)
                    || x.Idnp.Contains(item));
            }

            return items;
        }
    }
}
