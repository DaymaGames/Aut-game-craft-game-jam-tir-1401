using Pathfinding;
using UnityEngine;

public class AICustomScript : MonoBehaviour
{
    public Transform target;

    public MovementType movementType;

    public float speed = 200;

    public float nextWayPointDistance = 3;

    public bool reachedEndofPath;

    Path path;
    int currentWayPoint;

    Seeker seeker;

    new Rigidbody2D rigidbody;

    private void Awake()
    {
        if (speed == 0)
        {
            Debug.LogError("Speed Value Is zero");
        }

        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(UpdatePath), 0, 0.5f);


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
        //check if is there any path
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;

            if (movementType == MovementType.Velocity)
                rigidbody.velocity = Vector2.zero;
            
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector3 relative = path.vectorPath[currentWayPoint] - transform.position;
        
        if(movementType == MovementType.Force)
        {
            Vector3 force = speed * Time.fixedDeltaTime * relative.normalized;

            rigidbody.AddForce(force);
        }
        else if(movementType == MovementType.Velocity)
        {
            Vector2 velocity = relative.normalized * speed;
            rigidbody.velocity = velocity;
        }

        float distance = Vector2.Distance(path.vectorPath[currentWayPoint], transform.position);

        if (distance <= nextWayPointDistance)
        {
            currentWayPoint++;
        }
        
    }
    void UpdatePath()
    {
        seeker.StartPath(transform.position, target.transform.position, OnPathCompleted);
    }

    public enum MovementType
    {
        Force,Velocity
    }
}
