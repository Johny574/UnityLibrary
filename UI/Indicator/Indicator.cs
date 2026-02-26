using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : PoolObject, IPoolObject<IndicatorData>, IPlayable
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _image;
    public void Bind(IndicatorData variant)
    {
        if (variant == null)
            return;

        _text.text = variant.Text;
        _image.sprite = variant.Sprite;
    }

    public void BindObject(object variant) => Bind((IndicatorData)variant);

    public void Play(Vector2 origin) {
        Vector2 start = origin;
        // Vector2 finish = origin + Random.insideUnitCircle * Random.Range(1f, 3f);
        // DOTween.Sequence()
        // .Append(DOTween.To(() => start, x =>  { 
        //     start = x; 
        //     transform.position = start; 
        // }, finish,1f )).OnComplete(() => gameObject.SetActive(false)).Play();
    }
}