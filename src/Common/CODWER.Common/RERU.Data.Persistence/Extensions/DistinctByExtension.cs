﻿using System;
using System.Collections.Generic;

namespace RERU.Data.Persistence.Extensions
{
    public static class DistinctByExtension
    {
        public static IEnumerable<TSource> DistinctBy2<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}