﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace CODWER.RERU.Identity.Web.Quickstart.Account
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = false;
        public static bool AutomaticRedirectAfterSignOut = true;

        public static string InvalidCredentialsErrorMessage = "Email sau parolă nevalidă";
    }
}
