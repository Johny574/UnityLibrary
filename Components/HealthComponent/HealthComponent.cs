using System;
using UnityEngine;

public class HealthComponent
{
    MonoBehaviour _behaviour;
    float _health = 100;
    // todo bar doesnt need to pool
    IPoolObject _bar;
    [SerializeField]  Sprite _damageIcon, _healIcon;
    [SerializeField] AudioSource _hitSfx, _deathSfx;
    public Action OnDeath;
    
    public void Initilize(float health)
    {
        _health = health;
    }

    public void Change(float amount) {
        _health += amount;
        _health = Mathf.Clamp(_health, 0, 100);

        if (_health >= 100 && _bar != null)
            ((MonoBehaviour)_bar).gameObject.SetActive(false);
            
        else if (_health > 0 && _health < 100) {
            if (_bar == null)   
                // CreateBarAsync(_health/100);

            _bar?.BindObject(_health / 100);
        }

        if (amount < 0)
        {
            _hitSfx.Play();
        }

        if (_health <= 0)
            Die();

        // CanvasEvents.CreateIndicator.Invoke(transform.position, new IndicatorData(amount.ToString("+#;-#;0"), DamageIcon));
    }

    // async void CreateBarAsync(float fill) => _bar = await CanvasEvents.CreateBar?.Invoke(_followtarget, fill);

    void Die() {
        if (_bar != null)
        {
            ((MonoBehaviour)_bar).gameObject.SetActive(false);
            _bar = null;
        }
        OnDeath?.Invoke();
        _behaviour.gameObject.SetActive(false);
    }
}