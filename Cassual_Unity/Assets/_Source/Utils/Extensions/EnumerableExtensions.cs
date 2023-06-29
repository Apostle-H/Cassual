using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            for (int i = 0; i < collection.Count(); i++)
                action?.Invoke(collection.ElementAt(i), i);
        }
        
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action?.Invoke(item);
        }
    }
}