using UnityEngine;
using UnityEngine.InputSystem;

public struct ShootInfo
{
    public float shootPercentage;
    public bool  critHit;
}

public class FpsShootingInfo : MonoBehaviour
{
    [SerializeField] private float _timeToHold   = 5.0f;
    [SerializeField] private float _critHitAngle = 45.0f;
    [SerializeField] private GameObject Dude;


    private InputAction _interactAction;
    private float       _timer = 0.0f;

    public float Percent      => Mathf.Clamp01(_timer / _timeToHold);
    public float CritHitAngle => _critHitAngle;
    public bool  CritHit { get; set; }

    private float? _shootPercentage;
    public ShootInfo? GetShootInfo()
    {
        ShootInfo? info = _shootPercentage.HasValue 
            ? new ShootInfo
            {
                shootPercentage = _shootPercentage.Value,
                critHit = CritHit
            } : null;

        _shootPercentage = null;

        return info;
    }

    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    public void UpdateShootingInfo()
    {
        if (_interactAction.IsPressed())
        {
            _timer += Time.deltaTime;
          
        }

        if (_interactAction.WasReleasedThisFrame())
        {
            _shootPercentage = Percent;
            _timer = 0.0f;
        }
    }
}
