using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberController : MonoBehaviour {

    [SerializeField] private BarberMovement movement;
    [SerializeField] private Damagable damageable;
    private bool isSwooping = false; // TODO: we can probably remove this when we implement the actual behavior
    [SerializeField] Vector3[] waypoints = { new Vector3(39f, 23f, 0f), new Vector3(21f, 23f, 0f) };
    int currentWaypoint = 0;

    int stage = 0;

    private void Start()
    {
        movement.MoveTo(waypoints[currentWaypoint]);
    }

    private void FixedUpdate()
    {

        //update stage
        if (damageable.currentHealth > 6)
        {
            stage = 0;
        }
        else if (damageable.currentHealth > 2)
        {
            stage = 1;
        }
        else
        {
            stage = 2;
        }

        // behave according to which stage
        if(stage == 0)
        {
            StageOne();
        }
        else if(stage == 1)
        {
            StageTwo();
        }
        else
        {
            StageThree();
        }

        // TODO: for testing, when done remove the below: move back and forth, swooping when the waypoint is reached
        if (movement.IsWaypointReached())
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            Debug.Log("Waypoint reached");
            if (isSwooping)
               movement.MoveTo(waypoints[currentWaypoint]);
            else
                movement.SwoopTo(waypoints[currentWaypoint]);
            isSwooping = !isSwooping;
        }
    }

    private void StageOne()
    {

    }

    private void StageTwo()
    {

    }

    private void StageThree()
    {

    }
}
