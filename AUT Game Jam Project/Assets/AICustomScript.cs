using Pathfinding;
using UnityEngine;

public class AICustomScript : MonoBehaviour
{
    public Transform target;

    [SerializeField] float speed;
    float nextWayPointDistance = 3;

    Path path;
    int currentWayPoint;
    bool reachedEndofPath;

    Seeker seeker;

    Rigidbody2D rigidbody;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        seeker.StartPath(transform.position, target.position, onPathCompleted);
    }
    void onPathCompleted(Path generatedPath)
    {
        if (!generatedPath.error)
        {

            path = generatedPath;
            currentWayPoint = 0;
        }
    }
    private void Update()
    {
        if (path == null)
            return;

    }
}
