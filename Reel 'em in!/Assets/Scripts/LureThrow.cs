using UnityEngine;

public class LureThrow : MonoBehaviour
{
    public Transform lure;  // Reference to the lure object (set this in the inspector)
    public float throwForce = 10f;  // Speed of the throw

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isTouching = false;

    void Update()
    {
        // Handle touch input on mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Start of touch (swipe initiation)
            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                touchStartPos = touch.position;
            }
            // End of touch (swipe completion)
            else if (touch.phase == TouchPhase.Ended && isTouching)
            {
                isTouching = false;
                touchEndPos = touch.position;
                ThrowLure();
            }
        }

        // Handle mouse input for testing in the Unity editor
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))  // Left mouse button down
        {
            isTouching = true;
            touchStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && isTouching)  // Left mouse button up
        {
            isTouching = false;
            touchEndPos = Input.mousePosition;
            ThrowLure();
        }
        #endif
    }

    void ThrowLure()
    {
        // Calculate swipe direction
        Vector3 swipeDirection = touchEndPos - touchStartPos;

        // Convert to 3D space (we’ll ignore the Z-axis for now, and use swipe direction to influence the X and Y axes)
        Vector3 throwDirection = new Vector3(swipeDirection.x, swipeDirection.y, swipeDirection.z).normalized;

        // Apply the force to throw the lure (you can attach this to a Rigidbody component)
        Rigidbody lureRb = lure.GetComponent<Rigidbody>();
        if (lureRb != null)
        {
            lureRb.velocity = throwDirection * throwForce;
        }

        // Optionally, you could move the lure to the player's position before throwing
        lure.position = transform.position;
    }
}
