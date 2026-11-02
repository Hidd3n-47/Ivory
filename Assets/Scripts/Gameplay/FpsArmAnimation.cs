using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FpsArmAnimation : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = 0.5f;
    [SerializeField] private float _maxRotationSpeed = 2.0f;
    [SerializeField] private Material _critHitMaterial;

    private MeshRenderer _renderer;
    private Material     _defaultMaterial;

    private InputAction     _interactAction;

    private Transform       _armSocket;
    private FpsShootingInfo _shootingInfo;

    private float _swingRotation = 0.0f;

    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");

        _shootingInfo   = GetComponent<FpsShootingInfo>();
    }

    public void Setup(Transform armSocket)
    {
        _swingRotation   = 0.0f;
        _armSocket       = armSocket;
        _armSocket.localRotation = Quaternion.identity;

        _renderer        = _armSocket.GetChild(0).GetComponent<MeshRenderer>();
        _defaultMaterial = _renderer.material;
    }

    public void UpdateAnimation()
    {
        if (_interactAction.IsPressed())
        {
            _swingRotation += -_shootingInfo.Percent * (_maxRotationSpeed - _minRotationSpeed) * Time.deltaTime;
            _swingRotation  = Mathf.Abs(_swingRotation) % 360.0f * Mathf.Sign(_swingRotation);

            _armSocket.localRotation = Quaternion.Euler(_swingRotation, 0.0f, 0.0f);

            _shootingInfo.CritHit    = -_shootingInfo.CritHitAngle < _swingRotation && _swingRotation < 0.0f;

            _renderer.material       = _shootingInfo.CritHit ? _critHitMaterial : _defaultMaterial;
        }

        if (_interactAction.WasReleasedThisFrame())
        {
            _swingRotation           = 0.0f;
            _armSocket.localRotation = Quaternion.identity;
            _renderer.material       = _defaultMaterial;
        }
    }
}
