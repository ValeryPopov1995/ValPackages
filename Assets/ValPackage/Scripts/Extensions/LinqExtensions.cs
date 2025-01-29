using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ValPackage.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable)
                action(t);

            return enumerable;
        }

        public static IOrderedEnumerable<T> OrderByDistance<T>(this IEnumerable<T> enumerable, Func<T, Vector3> from, Vector3 target)
        {
            return enumerable.OrderBy(e => Vector3.Distance(from(e), target));
        }

        public static bool Equal<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.OrderBy(i => i).SequenceEqual(second.OrderBy(i => i));
        }
    }
}