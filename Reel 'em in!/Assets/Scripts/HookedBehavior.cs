using UnityEngine;

public class HookedBehavior : MonoBehaviour
{
    public Transform lure; // The lure to hook onto
    public float hookSpeed = 5f; // Speed at which the hooked object follows the lure
    public Vector3 offset; // Offset between the hooked object and the lure

    private bool isHooked = false;

    void Update()
    {
        if (isHooked && lure != null)
        {
            // Move the hooked object towards the lure position
            Vector3 targetPosition = lure.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * hookSpeed);
        }
    }

    // Call this method to hook the object to the lure
    public void HookToLure()
    {
        isHooked = true;
    }

    // Call this method to unhook the object from the lure
    public void UnhookFromLure()
    {
        isHooked = false;
    }
}