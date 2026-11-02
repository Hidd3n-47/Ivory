using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
internal class InRangeDetector : MonoBehaviour
{
    private HashSet<Collider> _inRange = new();

    public HashSet<Collider> InRange      => _inRange;
    public int               InRangeCount => _inRange.Count;

    public void Reset()
    {
        _inRange.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        _inRange.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _inRange.Remove(other);
    }

    private void LateUpdate()
    {
        // OnTriggerExit doesn't trigger on component destruction or inactivation, therefore manually check this.
        _inRange.RemoveWhere(enemy => enemy == null || !enemy.gameObject.activeInHierarchy || !enemy.enabled);
    }
}
