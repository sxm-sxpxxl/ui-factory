using System;
using System.Collections.Generic;
using Sxm.UIFactory.Components;
using UnityEngine;

namespace Sxm.UIFactory
{
    public static partial class UIMeshFactory
    {
        private static MeshParameters BuildParameters(Dictionary<string, object> rawParameters)
        {
            string type = GetParameter<string>(rawParameters, "Type");
            Type foundParametersType = null;
            
            foreach (var parameterType in ComponentRegistry.Keys)
            {
                if (parameterType.Name == type)
                {
                    foundParametersType = parameterType;
                    break;
                }
            }
            
            if (foundParametersType == null)
            {
                throw new ArgumentException($"Parameters with type '{type}' was not registered!");
            }
            
            return (MeshParameters)Activator.CreateInstance(foundParametersType, rawParameters);
        }
        
        internal static T GetSimpleParameter<T>(Dictionary<string, object> rawParameters, string key) where T : struct
        {
            return GetParameter<T>(rawParameters, key);
        }
        
        internal static IList<T> GetListParameter<T>(Dictionary<string, object> rawParameters, string key) where T : struct
        {
            return GetParameter<IList<T>>(rawParameters, key);
        }
        
        internal static T GetMeshParameters<T>(Dictionary<string, object> rawParameters, string key) where T : MeshParameters
        {
            var innerParameters = GetParameter<Dictionary<string, object>>(rawParameters, key);
            return (T)BuildParameters(innerParameters);
        }
        
        private static T GetParameter<T>(Dictionary<string, object> rawParameters, string key)
        {
            object obj;
            
            if (key == "Type")
            {
                if (rawParameters.TryGetValue(key, out obj))
                    return (T) obj;
                
                throw new ArgumentException($"Parameter '{key}' was not found in {nameof(rawParameters)} and must be specified!");
            }
            
            string type = GetParameter<string>(rawParameters, "Type");
            if (rawParameters.TryGetValue(key, out obj))
            {
                if (obj is T parameter)
                    return parameter;
                
                throw new ArgumentException($"Parameter '{key}' of type '{type}' was not found in {nameof(rawParameters)} or has different type than '{typeof(T).Name}'!");
            }
            
            Debug.LogWarning($"Parameter '{key}' of '{type}' was not found in {nameof(rawParameters)} and used by default with '{default(T)}' value!");
            return default(T);
        }
    }
}