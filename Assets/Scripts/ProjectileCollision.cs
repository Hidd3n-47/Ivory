using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{ 
    [SerializeField] private GameObject impactParticlePrefab;

    private void OnTriggerEnter (Collider other)
    {
        Instantiate(impactParticlePrefab, transform.position, transform.rotation);
    }
}
