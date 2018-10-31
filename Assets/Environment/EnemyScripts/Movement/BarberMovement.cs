using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberMovement : MonoBehaviour {

    private Vector3 _waypoint;
    public Vector3 Waypoint // the current waypoint we're moving to
    {
        get { return _waypoint; }
        private set {
            _waypoint = value;
            if (_waypoint.x > transform.position.x)
            {
                transform.localScale = faceRight;
            }
            else
            {
                transform.localScale = faceLeft;
            }
        }
    }
    [SerializeField] private float MOVESPEED = .1f; // base movement speed
    [SerializeField] private float SWOOPSPEEDMULTIPLIER = 3f; // how much faster the barber travels while swooping
    private float currentMoveSpeed; // movement speed with modifiers such as swooping multiplier
    [SerializeField] private float SWOOPAMPLITUDE = 2f;
    private bool isSwooping = false;
    private float initialHeight;
    private float initialX; // for keeping track of where in a swoop we are
    private const float WAYPOINTTOLERANCE = .005f; // tolerance for when we've reached the waypoint, to avoid getting stuck due to floating point inaccuracies
    private Vector3 faceLeft;
    private Vector3 faceRight;

    private void Start()
    {
        currentMoveSpeed = MOVESPEED;
        faceLeft = Vector3.Scale(transform.localScale, new Vector3(1f, 1f, 1f));
        faceRight = Vector3.Scale(transform.localScale, new Vector3(-1f, 1f, 1f));

        Waypoint = transform.position;
        initialHeight = transform.position.y;

        // TODO: for testing, when done remove the below: move towards the target
        //Waypoint = new Vector3(16f, Waypoint.y, Waypoint.z);
    }

    public void MoveTo(float newX)
    {
        MoveTo(new Vector3(newX, Waypoint.y, Waypoint.z));
    }

    public void MoveTo(Vector3 newWaypoint)
    {
        Waypoint = newWaypoint;
        Debug.Log(Waypoint);
        currentMoveSpeed = MOVESPEED;
        isSwooping = false;
    }

    public void SwoopTo(float newX)
    {
        SwoopTo(new Vector3(newX, Waypoint.y, Waypoint.z));
    }

    public void SwoopTo(Vector3 newWaypoint)
    {
        Waypoint = newWaypoint;
        initialX = transform.position.x;
        currentMoveSpeed = MOVESPEED * SWOOPSPEEDMULTIPLIER;
        isSwooping = true;
    }

    public bool IsWaypointReached()
    {
        return isSwooping ? Mathf.Abs(Waypoint.x - transform.position.x) < WAYPOINTTOLERANCE : Vector3.Distance(transform.position, Waypoint) < WAYPOINTTOLERANCE;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoint, currentMoveSpeed);

        // swoop in an arc
        if(isSwooping)
        {
            float currentHeight = initialHeight;
            if(Waypoint.x != initialX)
                currentHeight -= SWOOPAMPLITUDE * Mathf.Abs(Mathf.Sin(Mathf.PI * Mathf.Abs((transform.position.x - initialX) / (Waypoint.x - initialX))));
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }
    }
}
