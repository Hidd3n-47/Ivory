using UnityEngine;
using UnityEngine.Splines;

public class Tower : MonoBehaviour
{
    [SerializeField] private SplineContainer _assigendSpline;

    public SplineContainer AssigendSpline { get => _assigendSpline; }

    public bool IsDestroyed { get; set; } = false;
}
