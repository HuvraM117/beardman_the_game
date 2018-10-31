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
        if (damageable.currentHealth == 7)
        {
            movement.SetSpeedIncreased();
            stage = 1;
        }
        else if(damageable.currentHealth == 3)
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
  
    }

    private void CycleWaypoints()
    {
        if (movement.IsWaypointReached())
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            if (isSwooping)
                movement.MoveTo(waypoints[currentWaypoint]);
            else
                movement.SwoopTo(waypoints[currentWaypoint]);
            isSwooping = !isSwooping;
        }
    }

    private void StageOne()
    {
        CycleWaypoints();
    }

    private void StageTwo()
    {
        CycleWaypoints();
    }

    private void StageThree()
    {

    }
}
