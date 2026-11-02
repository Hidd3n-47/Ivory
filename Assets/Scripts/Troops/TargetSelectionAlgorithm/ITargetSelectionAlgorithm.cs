using System.Collections.Generic;
using UnityEngine;

public interface ITargetSelectionAlgorithm
{
    public Transform Select(Transform parent, HashSet<Collider> objectsInRange);
}
