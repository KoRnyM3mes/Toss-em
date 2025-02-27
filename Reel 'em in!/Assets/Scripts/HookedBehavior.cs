using UnityEngine;
using UnityEngine.Events;

public class HookedBehavior : MonoBehaviour
{
    public float hookSpeed = 500f; // Speed at which the hooked object follows the lure
    public Vector3 offset; // Offset between the hooked object and the lure
    private Transform lure; // The lure to hook onto
    private bool isHooked = false;

    public UnityEvent HookedEvent, UnhookedEvent;

    void Start()
    {
        // Hard-code the lure object by finding it in the scene (ensure you have a GameObject named "Lure")
        lure = GameObject.Find("Lure").transform;
    }

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
        // Assign random values to the x and z components of the offset while keeping the y component the same
        offset = new Vector3(Random.Range(-1f, 1f), offset.y, Random.Range(-1f, 1f));
        HookedEvent.Invoke();
        isHooked = true;
    }

    // Call this method to unhook the object from the lure
    public void UnhookFromLure()
    {
        isHooked = false;
    }
}