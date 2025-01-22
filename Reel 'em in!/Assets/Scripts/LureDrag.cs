using UnityEngine;

public class LureDrag : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 objectStartPos;
    private bool isDragging = false;

    void Update()
    {
        // Detect touch input or mouse input (for PC)
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0)) // Touch or Left Mouse Button
        {
            Vector3 inputPosition;

            if (Input.touchCount == 1) // If using touch
            {
                inputPosition = Input.GetTouch(0).position;
            }
            else // If using mouse (for PC testing)
            {
                inputPosition = Input.mousePosition;
            }

            // If the touch (or mouse click) has just begun
            if (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Raycast to check if we clicked/tapped on the object
                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform) // If the object is clicked/tapped
                    {
                        isDragging = true;
                        touchStartPos = inputPosition;
                        objectStartPos = transform.position;
                    }
                }
            }
            // If the input is moving (either touch or mouse drag)
            else if ((Input.GetMouseButton(0) || Input.GetTouch(0).phase == TouchPhase.Moved) && isDragging)
            {
                Vector3 touchDelta = inputPosition - touchStartPos;

                // Convert the movement to world space
                Vector3 worldDelta = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.WorldToScreenPoint(transform.position).z)) - Camera.main.ScreenToWorldPoint(new Vector3(touchStartPos.x, touchStartPos.y, Camera.main.WorldToScreenPoint(transform.position).z));

                // Move the object in X and Z axes only (preserve Y position)
                transform.position = new Vector3(objectStartPos.x + worldDelta.x, transform.position.y, objectStartPos.z + worldDelta.z);
            }
            // If the touch or mouse button has ended
            else if ((Input.GetMouseButtonUp(0) || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
            {
                isDragging = false;
            }
        }
    }
}
