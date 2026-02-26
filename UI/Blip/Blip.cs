
using UnityEngine;
using UnityEngine.UI;

public class Blip : PoolObject, IPoolObject<Sprite>
{
    [SerializeField] Image _sprite;

    public void Bind(Sprite variant) => _sprite.sprite = variant;
    public void BindObject(object variant) => Bind((Sprite)variant);
}