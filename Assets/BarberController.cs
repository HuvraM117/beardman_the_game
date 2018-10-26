using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberController : MonoBehaviour {

    [SerializeField] private BarberMovement movement;
    [SerializeField] private Damagable damageable;
    private bool isSwooping = false; // TODO: we can probably remove this when we implement the actual behavior
    float[] waypoints = { 16f, 9.5f };
    int currentWaypoint = 0;

    private void FixedUpdate()
    {
        // TODO: for testing, when done remove the below: move back and forth, swooping when the waypoint is reached
        if (movement.IsWaypointReached())
        {
            Debug.Log("Waypoint reached");
            if (isSwooping)
               movement.MoveTo(waypoints[currentWaypoint]);
            else
                movement.SwoopTo(waypoints[currentWaypoint]);
            isSwooping = !isSwooping;
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}
