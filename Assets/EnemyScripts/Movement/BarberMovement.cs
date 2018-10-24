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
    [SerializeField] private float MOVESPEED = .1f;
    private Vector3 faceLeft;
    private Vector3 faceRight;

    private void Start()
    {
        faceLeft = Vector3.Scale(transform.localScale, new Vector3(1f, 1f, 1f));
        faceRight = Vector3.Scale(transform.localScale, new Vector3(-1f, 1f, 1f));

        Waypoint = transform.position;

        // TODO: for testing, when done remove the below: move towards the target
        Waypoint = new Vector3(16f, Waypoint.y, Waypoint.z);
    }

    public void MoveTo(Vector3 newWaypoint)
    {
        Waypoint = newWaypoint;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoint, MOVESPEED);

        // TODO: for testing, when done remove the below: move back and forth
        if(Waypoint.x == transform.position.x)
        {
            Waypoint = new Vector3((Waypoint.x == 16f) ? 9.5f : 16f, Waypoint.y, Waypoint.z);
        }
    }
}
