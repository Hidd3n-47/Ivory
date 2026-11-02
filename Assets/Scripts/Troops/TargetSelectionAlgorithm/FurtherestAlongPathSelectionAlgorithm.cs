using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FurtherestAlongPathSelectionAlgorithm : ITargetSelectionAlgorithm
{
    public Transform Select(Transform parent, HashSet<Collider> objectsInRange)
    {
        Transform target = null;
        float tValue     = 0.0f;

        foreach (var collider in objectsInRange)
        {
            var movement = collider.transform.parent.GetComponentInChildren<MovementState>();

            if (movement.PathProgressPecentage > tValue)
            {
                target = collider.transform;
                tValue = movement.PathProgressPecentage;
            }
        }

        return target;
    }
}
