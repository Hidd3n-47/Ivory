using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeReference,
    TargetAlgorithmSelector] private ITargetSelectionAlgorithm _algorithm;
    [SerializeField]         private InRangeDetector           _rangeDetector;

    private Transform _target;
    public  Transform Target => _target;

    void Update()
    {
        _target = _algorithm.Select(transform, _rangeDetector.InRange);
    }
}
