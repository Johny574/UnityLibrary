
using TMPro;
using UnityEngine;

public class Tag : PoolObject, IPoolObject<string>
{
    [SerializeField] TextMeshProUGUI _text;
    public void Bind(string variant) => _text.text = variant;
    public void BindObject(object variant) => Bind((string)variant);
}