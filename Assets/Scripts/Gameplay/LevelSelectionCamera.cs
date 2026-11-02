using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelectionCamera : MonoBehaviour
{
    [SerializeField] float _speed        = 1.0f;
    [SerializeField] float _maxZoomDelta = 10.0f;
    [SerializeField] float _mapSize      = 300.0f;

    private bool  _isDragging;
    private float _startX;

    public void LoadLevel(Transform clicked)
    {
        SceneManager.LoadScene(clicked.parent.name);
    }

    private void Awake()
    {
        _startX = transform.localPosition.x;
    }

    public void Update()
    {
        _isDragging = Input.GetMouseButton(0);
    }

    private void FixedUpdate()
    {
        if (!_isDragging) return;

        Vector3 difference = Input.mousePositionDelta;

        Vector3 desired = transform.position - transform.right * _speed * difference.x;
        desired.x = Mathf.Clamp(desired.x, _startX, _startX + _mapSize);

        transform.position = desired;
    }
}
