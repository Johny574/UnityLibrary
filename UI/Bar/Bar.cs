



using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour, IPoolObject<float>
{
    [SerializeField] Image _fill;

    public void Bind(float variant) => _fill.fillAmount = variant;

    public void BindObject(object variant) => Bind((float)variant);
}