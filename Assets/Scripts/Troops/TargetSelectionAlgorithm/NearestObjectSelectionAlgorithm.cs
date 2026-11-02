using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NearestObjectSelectionAlgorithm : ITargetSelectionAlgorithm
{
    public Transform Select(Transform parent, HashSet<Collider> objectsInRange)
    {
        Transform target             = null;
        float     minDistanceSquared = float.MaxValue;

        foreach (var collider in objectsInRange)
        {
            float distanceSquared = (collider.transform.position -  parent.transform.position).sqrMagnitude;

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                target = collider.transform;
            }
        }

        return target;
    }
}
