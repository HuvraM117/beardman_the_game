using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberController : MonoBehaviour {

    [SerializeField] private BarberMovement movement;
    [SerializeField] private Damagable damageable;
    private bool isSwooping = false; // TODO: we can probably remove this when we implement the actual behavior
    [SerializeField] Vector3[] waypoints = { new Vector3(39f, 23f, 0f), new Vector3(21f, 23f, 0f) };
    [SerializeField] GameObject[] traps = new GameObject[0];
    int currentWaypoint = 0;
    int currentTrap = 0;

    private int stage = 0;
    [SerializeField] private float timePerTrap = 2f; // the time a given trap stays active before cycling to the next one
    private float trapCyclePeriod; // the total time it takes to cycle between all traps

    private void Start()
    {
        trapCyclePeriod = traps.Length * timePerTrap;
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

    // move through each waypoint in a cycle
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

    // cycle through activating and deactivating traps
    private void CycleTraps()
    {
        Debug.Log((Time.timeSinceLevelLoad % trapCyclePeriod));
        if((Time.timeSinceLevelLoad % trapCyclePeriod) > (timePerTrap * (currentTrap+1)) || (currentTrap == traps.Length-1 && (Time.timeSinceLevelLoad % trapCyclePeriod) < timePerTrap))
        {
            Debug.Log("olo");
            traps[currentTrap].SetActive(false);
            currentTrap = (currentTrap + 1) % traps.Length;
            traps[currentTrap].SetActive(true);
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
        CycleWaypoints();
        CycleTraps();
    }
}
