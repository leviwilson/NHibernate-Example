using System;
using System.Collections.Generic;
using System.Linq;

namespace NH.Data.Testing
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void Times(this int howMany, Action<int> action)
        {
            Enumerable.Range(0, howMany).ForEach(action);
        }
    }
}
