using UnityEngine;
using UnityEngine.Events;

public class Reset : MonoBehaviour
{
    public UnityEvent OnReset;

    public void TriggerReset()
    {
        OnReset?.Invoke();
    }
}
