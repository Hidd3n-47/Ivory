using UnityEngine;
using UnityEngine.Events;

public class FpsCamera : MonoBehaviour
{
    public UnityEvent OnCritShot;

    [SerializeField] private float _mouseSensitivity = 100f;

    [SerializeField] private Transform _projectilePrefab;
    [SerializeField] private Transform _critProjectilePrefab;

    [SerializeField] private float _minProjectileSpeed = 5.0f;
    [SerializeField] private float _maxProjectileSpeed = 50.0f;

    [SerializeField] private float _horizontalBloomOnNonCrit = 30.0f;

    private FpsShootingInfo _shootingInfo;
    private Transform       _troopVisual;
    private Transform       _troopCamera;

    private float _xRotation = 0f;
    private float _yRotation = 0f;

    void Start()
    {
        _shootingInfo = GetComponent<FpsShootingInfo>();
    }

    public void Setup(Troop troop)
    {
        _troopVisual = troop.TroopVisual;
        _troopCamera = troop.FpsCamera;

        _xRotation = 0.0f;
        _yRotation = 0.0f;
    }

    public void UpdateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation  = Mathf.Clamp(_xRotation, -90f, 90f);

        _yRotation += mouseX;

        _troopCamera.parent.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0.0f);
        _troopVisual.localRotation        = Quaternion.Euler(     0.0f, _yRotation, 0.0f);

        ShootInfo? info = _shootingInfo.GetShootInfo();

        if (info.HasValue)
        {
            float projectileSpeedScale = _minProjectileSpeed + (_maxProjectileSpeed - _minProjectileSpeed) * info.Value.shootPercentage;

            var proj = Instantiate(info.Value.critHit ? _critProjectilePrefab : _projectilePrefab, _troopVisual.position, Quaternion.identity);

            float angle       = _shootingInfo.CritHit ? 0.0f : _horizontalBloomOnNonCrit;
            float randomAngle = Random.Range(-angle, angle);
            Vector3 direction = Quaternion.AngleAxis(randomAngle, _troopCamera.parent.up) * _troopCamera.parent.forward;

            direction.Normalize();

            proj.GetComponent<Rigidbody>().AddForce(direction * projectileSpeedScale, ForceMode.Impulse);

            if(info.Value.critHit)
            {
                OnCritShot?.Invoke();
            }
        }
    }

    public void DoSomethingToFpsCamera()
    {
        // Todo add code here about camera shake or something for crit shot.
        // Example:
        /*
         Camera cam = _troopCamera.GetComponent<Camera>();
         cam.Shake();
         */
    }
}
