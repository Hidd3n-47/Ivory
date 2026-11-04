using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{

    public ParticleSystem flash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ParticleSystem spawnedFlash = Instantiate(flash, transform.position, transform.rotation);
       // spawnedFlash.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
    }

  
}
