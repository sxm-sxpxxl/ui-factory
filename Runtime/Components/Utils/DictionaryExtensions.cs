using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sxm.UIFactory.Components
{
    internal static class DictionaryExtensions
    {
        public static T Get<T>(this IDictionary<string, object> rawData, string key)
        {
            if (rawData.TryGet<T>(key, out var result))
            {
                return result;
            }

            Debug.LogWarning($"Parameter '{key}' of '{typeof(T)}' was not found in {nameof(rawData)} and used by default with '{default(T)}' value!");
            return default;
        }

        public static T GetOrDefault<T>(this IDictionary<string, object> rawData, string key, T defaultValue)
        {
            return rawData.TryGet<T>(key, out var result) ? result : defaultValue;
        }

        private static bool TryGet<T>(this IDictionary<string, object> rawData, string key, out T result)
        {
            object obj;

            if (key == "Type")
            {
                if (rawData.TryGetValue(key, out obj))
                {
                    result = (T) obj;
                    return true;
                }

                throw new ArgumentException($"Parameter '{key}' was not found in {nameof(rawData)} and must be specified!");
            }

            var type = rawData.Get<string>("Type");
            if (rawData.TryGetValue(key, out obj))
            {
                if (obj is T parameter)
                {
                    result = parameter;
                    return true;
                }

                throw new ArgumentException($"Parameter '{key}' of type '{type}' was not found in {nameof(rawData)} or has different type than '{typeof(T).Name}'!");
            }

            result = default;
            return false;
        }
    }
}