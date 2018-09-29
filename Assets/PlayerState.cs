using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeardState { IDLE, EXTENDING, RETRACTING, PULLING };

// a state machine for the player.  Just beard states right now, but we can add other states as additional enums if need be
// putting it in it's own class encapsulates and extracts the underlying state nicely
public class PlayerState : MonoBehaviour {

    private static BeardState _currentBeardState = BeardState.IDLE;
    public static BeardState CurrentBeardState { get; set; }

}
