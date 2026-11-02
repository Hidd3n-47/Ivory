using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage = 20.0f;

    public float Damage { get => _damage; }

    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter()
    {
        Destroy(transform.parent.gameObject);
    }
}
