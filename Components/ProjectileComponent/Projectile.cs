using System;
using UnityEngine;

[Serializable]
public class Projectile : MonoBehaviour
{
    private float _travelTime = 0f;
    public float TravelDuration = 5f;
    public float TravelSpeed = 5f;
    public float Lookoffset = -90;
    public Vector3 Direction = Vector3.zero;

    // public bool HitDisable = true;
    
    // [SerializeField] private TrailRenderer _trialRenderer;
    // [SerializeField] private LayerMask _waterLayer;
    // [SerializeField] private AudioSource _audio;
    // [SerializeField] public AudioClip LaunchAudio, HitAudio;
    // [SerializeField] private AnimatorOverrideController _hitEffect;


    // public void Initilize<T>(T variant)
    // {
    //     Projectile _variant = (Projectile)(object)variant;
    //     GetComponent<SpriteRenderer>().sprite = _variant.GetComponent<SpriteRenderer>().sprite;
    //     GetComponent<Animator>().runtimeAnimatorController = null;

    //     if (_variant.GetComponent<Animator>().runtimeAnimatorController != null)
    //     {
    //         GetComponent<Animator>().runtimeAnimatorController = _variant.GetComponent<Animator>().runtimeAnimatorController;
    //         GetComponent<Animator>().SetBool("Open", true);
    //     }

    //     LaunchAudio = _variant.LaunchAudio;
    //     HitAudio = _variant.HitAudio;
    //     gameObject.layer = _variant.gameObject.layer;
    //     TravelSpeed = _variant.TravelSpeed;
    //     TravelDuration = _variant.TravelDuration;
    //     HitDisable = _variant.HitDisable;
    //     Type = _variant.Type;
    //     Lookoffset = _variant.Lookoffset;
    // }

    public void Launch(Vector3 position, Vector3 direction)
    {
        // _audio.clip = LaunchAudio;
        // _audio.Play();
        // _trialRenderer.Clear();
        Direction = direction;
        transform.position = position;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (_travelTime < TravelDuration)
        {
            _travelTime += Time.deltaTime;
        }
        else
        {
            // gameObject.SetActive(false);
            Destroy(gameObject);
            Direction = Vector2.zero;
            _travelTime = 0f;
            // _trialRenderer.Clear();
        }

        transform.LookAt(Direction);
        transform.position = transform.position + Direction * TravelSpeed * Time.deltaTime;
    }

    // public float Damage() => _source.Service<StatService>().Stats["Attack"].Find(x => x.Data == Type.ToString()).Counter().Count;
    // public GameObject Source() => _source.Behaviour.gameObject;
    // public void Hit(Collider2D collider)
    // {
    //     _audio.clip = HitAudio;
    //     _audio.Play();

    //     if (HitDisable)
    //     {
    //         GetComponent<Animator>().SetBool("Open", false);
    //         gameObject.SetActive(false);
    //     }
    // }

    // async void PlayHitEffect()
    // {
    //     GameObject effect = await CentralManager.Instance.Manager<ObjectManager>().Pooler(Pooler.Type.Object, "FX").GetObject();
    //     effect.transform.position = transform.position;
    //     effect.GetComponent<Animator>().runtimeAnimatorController = _hitEffect;
    // }
    // public List<BuffData> Buffs() => _debuffs;
}