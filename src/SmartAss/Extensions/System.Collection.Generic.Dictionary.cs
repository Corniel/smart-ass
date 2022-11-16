using SmartAss;

namespace System.Collections.Generic;

public static class SmartAssDictonaryExtensions
{
    public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> create)
    {
        Guard.NotNull(dictionary, nameof(dictionary));

        if(!dictionary.TryGetValue(key, out var value))
        {
            value = create();
            dictionary[key] = value;
        }
        return value;
    }
}

