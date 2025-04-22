using UnityEngine;

public class EditorLikeCameraMovement : MonoBehaviour
{
	public float moveSpeed = 10f; // Speed of camera movement
	public float lookSpeed = 100f; // Speed of camera rotation
	public float zoomSpeed = 10f; // Speed of camera zoom

	private Vector3 _startMousePosition;
	private Vector3 _startRotation;

	void Update()
	{
		// Right-click to rotate the camera
		if (Input.GetMouseButtonDown(1)) // Right mouse button
		{
			_startMousePosition = Input.mousePosition;
			_startRotation = transform.eulerAngles;
		}

		if (Input.GetMouseButton(1)) // Right mouse button held down
		{
			Vector3 currentMousePosition = Input.mousePosition;
			Vector3 deltaMousePosition = currentMousePosition - _startMousePosition;

			// Calculate new rotation
			float newRotationX = _startRotation.x - deltaMousePosition.y * lookSpeed * Time.deltaTime;
			float newRotationY = _startRotation.y + deltaMousePosition.x * lookSpeed * Time.deltaTime;

			// Apply rotation
			transform.eulerAngles = new Vector3(newRotationX, newRotationY, 0);
		}

		// Mouse wheel to zoom in/out
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll != 0)
		{
			transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);
		}

		// Middle mouse button to move the camera
		if (Input.GetMouseButton(2)) // Middle mouse button held down
		{
			Vector3 moveDirection = Vector3.zero;

			// Get mouse movement delta
			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = Input.GetAxis("Mouse Y");

			// Move the camera based on mouse movement
			moveDirection -= new Vector3(mouseX, mouseY, 0) * moveSpeed * Time.deltaTime;

			// Apply movement
			transform.Translate(moveDirection, Space.Self);
		}
	}
}