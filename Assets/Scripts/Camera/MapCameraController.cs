using UnityEngine;

namespace CityPresident.Rendering
{
    /// <summary>Basic mobile map camera: one-finger pan and two-finger pinch zoom.</summary>
    public sealed class MapCameraController : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        [SerializeField] private float panSensitivity = 0.8f;
        [SerializeField] private float zoomSpeed = 0.02f;
        [SerializeField] private float minHeight = 12f;
        [SerializeField] private float maxHeight = 180f;
        [SerializeField] private Vector2 worldBounds = new Vector2(250f, 250f);

        private Vector2 previousTouch;
        private float previousPinchDistance;

        private void Awake()
        {
            if (targetCamera == null) targetCamera = GetComponent<Camera>();
            if (targetCamera == null) targetCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (targetCamera == null) return;

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) previousTouch = touch.position;
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 delta = touch.position - previousTouch;
                    Pan(delta);
                    previousTouch = touch.position;
                }
            }
            else if (Input.touchCount >= 2)
            {
                Touch a = Input.GetTouch(0);
                Touch b = Input.GetTouch(1);
                float distance = Vector2.Distance(a.position, b.position);

                if (a.phase == TouchPhase.Began || b.phase == TouchPhase.Began)
                    previousPinchDistance = distance;
                else
                {
                    float delta = distance - previousPinchDistance;
                    Zoom(delta);
                    previousPinchDistance = distance;
                }
            }
            else if (Input.touchCount == 0 && Input.GetMouseButton(0))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                Pan(new Vector2(x * 20f, y * 20f));
            }

            if (Input.touchCount == 0)
            {
                float scroll = Input.mouseScrollDelta.y;
                if (Mathf.Abs(scroll) > 0.01f) Zoom(scroll * 40f);
            }

            ClampPosition();
        }

        private void Pan(Vector2 screenDelta)
        {
            Vector3 right = targetCamera.transform.right;
            Vector3 forward = Vector3.ProjectOnPlane(targetCamera.transform.forward, Vector3.up).normalized;
            Vector3 movement = (-right * screenDelta.x - forward * screenDelta.y)
                               * panSensitivity * targetCamera.transform.position.y * 0.002f;
            targetCamera.transform.position += movement;
        }

        private void Zoom(float delta)
        {
            Vector3 position = targetCamera.transform.position;
            position.y = Mathf.Clamp(position.y - delta * zoomSpeed, minHeight, maxHeight);
            targetCamera.transform.position = position;
        }

        private void ClampPosition()
        {
            Vector3 position = targetCamera.transform.position;
            position.x = Mathf.Clamp(position.x, -worldBounds.x, worldBounds.x);
            position.z = Mathf.Clamp(position.z, -worldBounds.y, worldBounds.y);
            targetCamera.transform.position = position;
        }
    }
}
