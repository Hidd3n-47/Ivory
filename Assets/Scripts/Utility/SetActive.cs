using UnityEngine;

public class SetActive : MonoBehaviour
{
    public void SetObjectActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void NegateSetObjectActive(bool active)
    {
        gameObject.SetActive(!active);
    }
}
