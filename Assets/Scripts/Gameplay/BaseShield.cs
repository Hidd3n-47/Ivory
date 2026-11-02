using System.Linq;
using UnityEngine;


public class BaseShield : MonoBehaviour
{
    [SerializeField] EnemyTroopContainer _troopContainer;

    void Update()
    {
        gameObject.SetActive(!(_troopContainer.AllTowersDestroyed && _troopContainer.AllDefencesDestroyed));
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.transform.parent.GetComponentInChildren<Damageable>();
        health?.Damage(1_000_000);
    }
}
