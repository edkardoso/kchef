using System.Collections.Generic;

namespace edk.Kchef.Domain.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddIfNotContains<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                return;

            dictionary.Add(key, value);

        }
    }
}
