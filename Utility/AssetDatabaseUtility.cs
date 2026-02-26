using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class AssetDatabaseUtility
{
    public static List<T> GetAllAssetsByType<T>() where T : Object
    {
        List<T> assets = new List<T>();

        // Search for all assets of type T
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }
    public static List<GameObject> GetPrefabsWithSubclass(System.Type subclass)
    {
        List<GameObject> results = new List<GameObject>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                var components = prefab.GetComponents<Component>();
                foreach (var comp in components)
                {
                    if (comp == null) continue;

                    System.Type type = comp.GetType();
                    // Check if this component inherits from FriendlyBuildingBehaviour<>
                    if (IsSubclassOfRawGeneric(subclass, type))
                    {
                        results.Add(prefab);
                        break; // no need to check further components
                    }
                }
            }
        }

        return results;
    }

       private static bool IsSubclassOfRawGeneric(System.Type generic, System.Type toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }

    public static List<GameObject> GetPrefabsWithComponent<T>() where T : Component
    {
        List<GameObject> results = new List<GameObject>();

        // Find all prefabs in the project
        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null && prefab.GetComponent<T>() != null)
            {
                results.Add(prefab);
            }
        }

        return results;
    }


}