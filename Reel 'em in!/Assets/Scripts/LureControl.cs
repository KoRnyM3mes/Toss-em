using UnityEngine;
public class LureControl : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove; // The object that will be moved
    [SerializeField] private float moveForce = 10f;   // The force applied when moving left or right
    [SerializeField] private float zForce = 5f;       // The constant force applied to the Z-axis
    private Rigidbody rb;

    void Start()
    {
        // Ensure the objectToMove has a Rigidbody component
        if (objectToMove != null)
        {
            rb = objectToMove.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogWarning("objectToMove is not assigned!");
        }
    }

    void Update()
    {

         ApplyZForce();

        // For mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the user has tapped on the screen
            if (touch.phase == TouchPhase.Began)
            {
                HandleInput(touch.position);
            }
        }

        // For mouse input (PC)
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            HandleInput(mousePosition);
        }
    }

    private void HandleInput(Vector2 inputPosition)
    {
        // Apply left or right movement based on the touch/click position
        if (inputPosition.x < Screen.width / 2)
        {
            // Tap/click on the left side, move the object to the left
            MoveObject(Vector3.left);
        }
        else
        {
            // Tap/click on the right side, move the object to the right
            MoveObject(Vector3.right);
        }
    }

    private void MoveObject(Vector3 direction)
    {
        if (rb != null)
        {
            // Apply force in the left-right direction
            rb.AddForce(direction * moveForce, ForceMode.Impulse);
        }
    }

    private void ApplyZForce()
    {
        if (rb != null)
        {
            // Apply constant force on the Z-axis (forward or backward)
            rb.AddForce(-(Vector3.forward) * zForce, ForceMode.Force);
        }
    }
}