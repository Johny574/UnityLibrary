using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver, IEnumerable<KeyValuePair<TKey, TValue>>
{
    [field:SerializeField] public List<TKey> Keys { get; private set; }= new();
    [field:SerializeField] public List<TValue> Values { get; private set; }= new();
    private Dictionary<TKey, TValue> dictionary = new();

    public TValue this[TKey key] {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    public void Add(TKey key, TValue value) => dictionary.Add(key, value);
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void OnBeforeSerialize() {
        if (!Application.isPlaying)
            return;

        Keys.Clear();
        Values.Clear();
        foreach (var kvp in dictionary) {
            Keys.Add(kvp.Key);
            Values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize() {
        dictionary.Clear();
        for (int i = 0; i < Math.Min(Keys.Count, Values.Count); i++)
            dictionary[Keys[i]] = Values[i];
    }
}
