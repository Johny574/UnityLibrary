using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : Singleton<Tracker>
{
    Dictionary<Type, List<GameObject>> _objects = new();
    Dictionary<Type, Dictionary<int, GameObject>> _uniqueObjects = new();

    protected override void Awake() {
        base.Awake();
        _objects = new();
        _uniqueObjects = new();
    }
    /// <summary>
    /// Gets the closest object to our origin.
    /// </summary>
    /// <param name="objects">List of objects to pan through</param>
    /// <param name="origin">The origin point</param>
    /// <returns></returns>
    public GameObject GetClosestObject(List<GameObject> objects, Vector2 origin) {
        float closest = (origin - (Vector2)objects.First().transform.position).sqrMagnitude;
        float distance;
        int closestIndex = 0;

        for (int i = 0; i < objects.Count; i++) {
            distance = Vector2.Distance(origin, objects[i].transform.position);
            if (distance < closest) {
                closest = distance;
                closestIndex = i;
            }
        }
        return objects[closestIndex];
    }

    /// <summary>
    /// Registers a object for tracking that has a unique ID.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="obj">Object to register</param>
    /// <param name="UID">Unique ID</param>
    public void RegisterUnique<T>(GameObject obj, int UID) {
        if (!_uniqueObjects.ContainsKey(typeof(T)))
            _uniqueObjects.Add(typeof(T), new());

        if (_uniqueObjects[typeof(T)].ContainsKey(UID))
            return;

        _uniqueObjects[typeof(T)].Add(UID, obj);
        Register<T>(obj);
    }

    /// <summary>
    /// Removes a object with a unique ID that was kept track of.
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <param name="UID">Unique ID</param>
    public void UnregisterUnique<T>(int UID)
    {
        if (!_uniqueObjects.ContainsKey(typeof(T)))
            return;

        GameObject GameObject = _uniqueObjects[typeof(T)][UID];
        _uniqueObjects[typeof(T)].Remove(UID);
        Unregister<T>(GameObject);
    }
    /// <summary>
    /// Grabs a unique object by type.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="UID">Unique ID</param>
    /// <returns></returns>
    public GameObject GetUnique<T>(int UID) => _uniqueObjects[typeof(T)].Where(x => x.Key == UID).First().Value;

    /// <summary>
    /// Register a object to be kept track of.
    /// </summary>
    /// <typeparam name="T">The object type</typeparam>
    /// <param name="obj">The object</param>
    public void Register<T>(GameObject obj)
    {
        if (!_objects.ContainsKey(typeof(T)))
            _objects.Add(typeof(T), new List<GameObject>());

        _objects[typeof(T)].Add(obj);
    }
    /// <summary>
    ///  Unregister a object to keep track of.
    /// </summary>
    /// <typeparam name="T">The type of object</typeparam>
    /// <param name="obj">The object</param>
    public void Unregister<T>(GameObject obj)
    {
        if (!_objects.ContainsKey(typeof(T)))
            return;

        _objects[typeof(T)].Remove(obj);

        if (_objects[typeof(T)].Count <= 0)
            _objects.Remove(typeof(T));
    }
    /// <summary>
    /// Clears all object that was kept track of.
    /// </summary>
    public void Clear()
    {
        _objects.Clear();
        _uniqueObjects.Clear();   
    }
}