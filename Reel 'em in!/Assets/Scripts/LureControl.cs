using UnityEngine;

public class LureControl : MonoBehaviour
{
    private Vector3 touchStartPos;  // Position where the touch started
    private Vector3 objectStartPos; // The initial position of the lure
    private bool isDragging = false; // Whether the lure is being dragged
    private Rigidbody rb;           // Reference to the Rigidbody

    void Start()
    {
        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for touch input (or mouse input for testing)
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0)) // Detect touch or mouse click
        {
            Vector3 inputPosition;

            // Determine if using touch or mouse input
            if (Input.touchCount == 1) 
            {
                inputPosition = Input.GetTouch(0).position;
            }
            else 
            {
                inputPosition = Input.mousePosition;
            }

            // If touch begins or mouse button is pressed
            if (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Raycast to detect if the user tapped the lure object
                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform) // If the object clicked is the lure
                    {
                        isDragging = true;
                        touchStartPos = inputPosition; // Store the start position of the touch
                        objectStartPos = transform.position; // Store the object's initial position
                    }
                }
            }
        }

        // Handle dragging based on screen side
        if ((Input.GetMouseButton(0) || Input.GetTouch(0).phase == TouchPhase.Moved) && isDragging)
        {
            Vector3 inputPosition;

            // Get the input position based on touch or mouse
            if (Input.touchCount == 1)
            {
                inputPosition = Input.GetTouch(0).position;
            }
            else
            {
                inputPosition = Input.mousePosition;
            }

            // Check which side of the screen the touch is on
            float screenWidth = Screen.width;
            bool isLeftSide = inputPosition.x < screenWidth / 2; // Left side of the screen
            bool isRightSide = inputPosition.x >= screenWidth / 2; // Right side of the screen

            // Calculate the movement in the X direction
            float xMove = inputPosition.x - touchStartPos.x;

            // Apply movement based on the side of the screen
            if (isLeftSide)
            {
                // Apply the velocity to move leftward
                rb.velocity = new Vector3(-xMove * 0.1f, rb.velocity.y, rb.velocity.z);
            }
            else if (isRightSide)
            {
                // Apply the velocity to move rightward
                rb.velocity = new Vector3(xMove * 0.1f, rb.velocity.y, rb.velocity.z);
            }
        }

        // Stop dragging when the touch ends or mouse button is released
        if ((Input.GetMouseButtonUp(0) || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) && isDragging)
        {
            isDragging = false;
            rb.velocity = Vector3.zero; // Stop the movement when drag ends
        }
    }
}