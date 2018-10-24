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
    private const float WAYPOINTTOLERANCE = .001f; // tolerance for when we've reached the waypoint, to avoid getting stuck due to floating point inaccuracies
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
        Waypoint = new Vector3(16f, Waypoint.y, Waypoint.z);
    }

    public void MoveTo(Vector3 newWaypoint)
    {
        Waypoint = newWaypoint;
        currentMoveSpeed = MOVESPEED;
        isSwooping = false;
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
        return Mathf.Abs(Waypoint.x - transform.position.x) < WAYPOINTTOLERANCE;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoint, currentMoveSpeed);

        // swoop in an arc
        if(isSwooping)
        {
            float currentHeight = initialHeight - SWOOPAMPLITUDE * Mathf.Abs(Mathf.Sin(Mathf.PI * Mathf.Abs((transform.position.x - initialX) / (Waypoint.x - initialX))));
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }

        Debug.Log("Waypoint: " + Waypoint.x + "Transform: " + transform.position.x);
        // TODO: for testing, when done remove the below: move back and forth, swooping when the waypoint is reached
        if(IsWaypointReached())
        {
            Debug.Log("Waypoint reached");
            if(isSwooping)
                MoveTo(new Vector3((Mathf.Abs(Waypoint.x - 16f) < WAYPOINTTOLERANCE) ? 9.5f : 16f, Waypoint.y, Waypoint.z));
            else
                SwoopTo(new Vector3((Mathf.Abs(Waypoint.x - 16f) < WAYPOINTTOLERANCE) ? 9.5f : 16f, Waypoint.y, Waypoint.z));
        }
    }
}
