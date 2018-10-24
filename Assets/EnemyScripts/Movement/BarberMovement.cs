using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberMovement : MonoBehaviour {

    private Vector3 waypoint; // the current waypoint we're moving to
    [SerializeField] private float MOVESPEED = .1f;

    private void Start()
    {
        waypoint = transform.position;

        // TODO: for testing, when done remove the below: move towards the target
        waypoint = new Vector3(16f, waypoint.y, waypoint.z);
    }

    public void MoveTo(Vector3 newWaypoint)
    {
        waypoint = newWaypoint;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoint, MOVESPEED);
    }
}
