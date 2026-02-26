using System;
using System.Collections.Generic;

[Serializable]
public class ResourceManager : Singleton<ResourceManager> {
    Dictionary<Type, Array> _resources;
    protected override void Awake() {
        base.Awake();
        _resources = new() { 
        // { typeof(Recipe), UnityEngine.Resources.LoadAll<Recipe>("Recipe") },
        };
    }

    public Array Resource<T>() {
        if (!_resources.ContainsKey(typeof(T)))
            throw new Exception("Resource not found");

        return _resources[typeof(T)];
    }
}
