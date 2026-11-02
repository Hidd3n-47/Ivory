using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _duration;

    void Start()
    {
        Destroy(gameObject, _duration);
    }
}
