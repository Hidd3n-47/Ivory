using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float _health = 100.0f;
    public float Health           => _health;
    public float HealthPercentage => _health / _startHealth * 100.0f;

    public UnityEvent OnHeal;
    public UnityEvent OnDamage;
    public UnityEvent OnDestroyed;

    private float _startHealth;

    private bool _destroyed     = false;
    private bool _shouldDestroy = false;

    public void Reset()
    {
        _health        = _startHealth;
        _destroyed     = false;
        _shouldDestroy = false;
    }

    private void Awake()
    {
        _startHealth = _health;
    }

    private void LateUpdate()
    {
        if (_shouldDestroy)
        {
            transform.parent.gameObject.SetActive(false);

            _shouldDestroy = false;
        }
    }

    public void Damage(Projectile projectile)
    {
        Damage(projectile.Damage);
    }

    public void Damage(float damage)
    {
        if (_destroyed)
        {
            return;
        }

        _health -= damage;

        if (_health <= 0.0f)
        {
            _health = 0.0f;
            OnDestroyed?.Invoke();

            _destroyed = true;
        }
        else
        {
            OnDamage?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (_destroyed)
        {
            return;
        }

        _health += amount;

        _health = Mathf.Min(_health, _startHealth);

        OnHeal?.Invoke();
    }

    public void DestroyObject()
    {
        _shouldDestroy = true;
    }
}
