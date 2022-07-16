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
        if (speed == 0)
        {
            Debug.LogError("Speed Value Is zero");
        }
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0, 0.5f);


    }
    void OnPathCompleted(Path generatedPath)
    {
        if (!generatedPath.error)
        {

            path = generatedPath;
            currentWayPoint = 0;
        }
    }
    private void FixedUpdate()
    {
        
        if (path == null)
            return;
        //check for if there is a path
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector3 direction = (path.vectorPath[currentWayPoint] - transform.position).normalized ;
        Vector3 force = direction * speed * Time.deltaTime;
        rigidbody.AddForce(force);
        float distance = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);

        if (distance <= nextWayPointDistance)
        {
            currentWayPoint++;
        }
        
    }
    void UpdatePath()
    {
        seeker.StartPath(transform.position, target.transform.position, OnPathCompleted);
    }
}
