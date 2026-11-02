using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ProjectileInteractable : MonoBehaviour
{
    public UnityEvent<Projectile> OnProjectileTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        var projectile = other.GetComponent<Projectile>();

        if(!projectile)
        {
            return;
        }

        OnProjectileTriggerEnter?.Invoke(projectile);

        Destroy(projectile.transform.parent.gameObject);
    }
}
