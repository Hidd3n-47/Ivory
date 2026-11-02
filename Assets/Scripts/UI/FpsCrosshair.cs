using UnityEngine;

public class FpsCrosshair : MonoBehaviour
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Transform _crosshairBeingScaled;

    [SerializeField] private float _startScale = 1.5f;
    [SerializeField] private float _endScale   = 0.5f;

    private FpsShootingInfo _shootingInfo;

    public void Activate()
    {
        _crosshair.gameObject.SetActive(true);
        _crosshairBeingScaled.localScale = new Vector3(_startScale, _startScale, _startScale);
    }

    public void Deactivate()
    {
        _crosshairBeingScaled.localScale = new Vector3(_startScale, _startScale, _startScale);
        _crosshair.gameObject.SetActive(false);
    }

    private void Start()
    {
        _shootingInfo = GetComponent<FpsShootingInfo>();
    }

    public void UpdateCrosshair()
    {
        float scale = _startScale + _shootingInfo.Percent * (_endScale - _startScale);
        _crosshairBeingScaled.localScale = new Vector3(scale, scale, 1.0f);
    }
}
