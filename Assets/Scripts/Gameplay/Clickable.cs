using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent<Transform> OnClicked;

    [SerializeField] private LayerMask _clickableLayer;
    [SerializeField] private float     _rayDistance = 100.0f;

    [SerializeField] private Texture2D _cursorOpen;
    [SerializeField] private Texture2D _cursorAttack;
    [SerializeField] private Texture2D _cursorDrag;

    private void Awake()
    {
        Cursor.SetCursor(_cursorOpen, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (!Camera.main || !Camera.main.gameObject.activeInHierarchy)
        {
            return;
        }

        var  ray         = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool rayCastHit  = Physics.Raycast(ray.origin, ray.direction, out var hit, _rayDistance, _clickableLayer);
        bool mouseDown   = Input.GetMouseButton(0);

        Texture2D cursor = rayCastHit ? _cursorAttack : mouseDown ? _cursorDrag : _cursorOpen;

        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);

        if (!mouseDown)
        {
            return;
        }

        if (rayCastHit && Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke(hit.transform);

            hit.collider.gameObject.GetComponent<IOnClick>()?.OnClick();
        }
    }
}
