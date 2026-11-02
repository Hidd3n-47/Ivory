using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    public float speed = 1.0f;
    public float zoomSpeed = 1.0f;
    public float maxZoomDelta = 10.0f;

    private Camera mainCam;
    private bool isDragging;
    public float minSize = 6f;
    public float maxSize = 16f;
    private float startZ;

    private void Awake()
    {
        mainCam = Camera.main;
        startZ = transform.localPosition.z;
    }

    public void Update()
    {
        isDragging        = Input.GetMouseButton(0);
        float scrollInput = Input.mouseScrollDelta.y;

        if (scrollInput != 0)
        {
            float newSize = mainCam.orthographicSize - (scrollInput * zoomSpeed);
            mainCam.orthographicSize = Mathf.Clamp(newSize, minSize, maxSize);
        }

        Vector2 difference = Input.mouseScrollDelta;
        var desired = transform.position + transform.forward * zoomSpeed * difference.y;

        // Only zoom in if the new position is less than the max delta ("distance from starting value").
        if (Mathf.Abs(desired.z - startZ) < maxZoomDelta)
        {
            transform.position = desired;
        }

        if (!isDragging) return;
        difference = Input.mousePositionDelta;

        transform.position -= transform.right * speed * difference.x;
        // Cross product to remove the pitch rotation from the camera to get a "world" forward relative to the camera.
        // Projection to project the forward movement onto the "world" so that we move forwards without the pitch rotation.
        // Negate to make dragging opposite.
        transform.position -= Vector3.Project(transform.forward, Vector3.Cross(transform.right, Vector3.up)) * speed * difference.y;
    }
}
