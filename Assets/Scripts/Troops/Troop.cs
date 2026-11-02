using UnityEngine;

public class Troop : MonoBehaviour
{
    [SerializeField] private Transform _fpsCamera   = null;
    [SerializeField] private Transform _armRotation = null;
    [SerializeField] private Transform _troopVisual = null;

    public Transform FpsCamera   => _fpsCamera;
    public Transform ArmRotation => _armRotation;
    public Transform TroopVisual => _troopVisual;

    public float MovementSpeedMultiplier { get; set; }  = 1.0f;
}
