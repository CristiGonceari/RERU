using System;

namespace CODWER.RERU.Personal.Application.Enums
{
    public class AuthorizationVariables
    {
        // Password validity in days
        public readonly static int PasswordValidity = 30;

        // Salt used to encrypt password
        public readonly static string Salt = "J2hrGsTUL~9+;QE~8ZZ5V";

        // Security code validity in seconds
        public readonly static double SecuityCodeValidity = TimeSpan.FromMinutes(5).TotalSeconds;

        public readonly static double CookieExpiration = TimeSpan.FromDays(1).TotalHours;
    }
}
