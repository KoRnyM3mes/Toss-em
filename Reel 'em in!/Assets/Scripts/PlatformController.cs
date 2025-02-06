using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public enum PathType
    {
        Linear,
        Looping
    }

    [Header("Platform Settings")]
    public float speed = 2f;
    public PathType pathType = PathType.Linear;

    [Header("Path Points")]
    public Transform[] pathPoints;

    private int currentPointIndex = 0;
    private bool isMoving = false;
    private int direction = 1; // 1 for forward, -1 for backward


    void Start()
    {
        StartMovement();
    }
    void Update()
    {
        if (isMoving && pathPoints.Length > 1)
        {
            MovePlatform();
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    public void PauseMovement()
    {
        isMoving = false;
    }

    public void ReverseDirection()
    {
        direction *= -1;
    }

    private void MovePlatform()
    {
        Transform targetPoint = pathPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            UpdatePointIndex();
        }
    }

    private void UpdatePointIndex()
    {
        if (pathType == PathType.Linear)
        {
            currentPointIndex += direction;

            if (currentPointIndex >= pathPoints.Length || currentPointIndex < 0)
            {
                direction *= -1; // Reverse direction
                currentPointIndex += direction;
            }
        }
        else if (pathType == PathType.Looping)
        {
            currentPointIndex = (currentPointIndex + direction) % pathPoints.Length;
            if (currentPointIndex < 0)
            {
                currentPointIndex += pathPoints.Length;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < pathPoints.Length; i++)
        {
            if (i < pathPoints.Length - 1)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
            }
            else if (pathType == PathType.Looping)
            {
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[0].position);
            }
        }

        Gizmos.color = Color.red;
        foreach (var point in pathPoints)
        {
            Gizmos.DrawSphere(point.position, 0.1f);
        }
    }
}
