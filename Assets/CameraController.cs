using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2f; // Mouse sensitivity for rotation & panning
    public float zoomSpeed = 10f;  // Speed of zooming
    public float minZoom = 5f;     // Min zoom distance
    public float maxZoom = 50f;    // Max zoom distance

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        Vector3 delta = Vector3.zero;

        // Right click function (rotate camera)
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            delta = Input.mousePosition - lastMousePosition;
            float rotationX = -delta.y * sensitivity;  // Invert Y-axis rotation
            float rotationY = delta.x * sensitivity;

            transform.eulerAngles += new Vector3(rotationX, rotationY, 0);
        }

        // Middle button function (move camera)
        if (Input.GetMouseButton(2)) // Middle mouse button held down
        {
            delta = Input.mousePosition - lastMousePosition;
            Vector3 pan = new Vector3(-delta.x, -delta.y, 0) * sensitivity * 0.01f;
            transform.Translate(pan, Space.Self);
        }

        // Middle button scroll function (zoom in and out)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 zoomDirection = transform.forward * scroll * zoomSpeed;
            Vector3 newPosition = transform.position + zoomDirection;

            // Limit zoom within min/max range
            float distance = Vector3.Distance(newPosition, Vector3.zero);
            if (distance > minZoom && distance < maxZoom)
            {
                transform.position = newPosition;
            }
        }

        lastMousePosition = Input.mousePosition; // Update last position
    }
}
