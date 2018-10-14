using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeardState { IDLE, EXTENDING, RETRACTING, PULLING };

// a state machine for the player
// putting it in it's own class encapsulates and extracts the underlying state nicely
public class PlayerState : MonoBehaviour {

    public float currentHealth = 10f;
    public static float CurrentHealth { get; private set; }

    private static BeardState _currentBeardState = BeardState.IDLE;
    public static BeardState CurrentBeardState { get; set; }

    // setting up health and beard length as properties this way automatically ensures that increasing one decreases the other and vice versa
    private static float health = 5f;
    public static float Health {
        get { return health; }
        private set {
            beardLength += health - value;
            health = value;
        }
    }

    private static float beardLength = 2f;
    public static float BeardLength {
        get { return beardLength; }
        private set
        {
            health += beardLength - value;
            beardLength = value;
        }
    }

    private const float BEARDGROWTHRATE = .02f;

    // beard length + health, the total resource, I can't think of what else to call it so hopefully someone else can
    public static float vitality
    {
        get
        {
            return beardLength + health;
        }
    }

    //Increases the maximum attack range
    public void growBeard()
    {
        if (BeardLength < 3f && health - BEARDGROWTHRATE > 0)
            BeardLength = BeardLength + BEARDGROWTHRATE;
        Debug.Log("health: " + health + "length: " + beardLength + "vitality: " + vitality);
    }

    //Shrinks the maximum attack range
    public void shrinkBeard()
    {
        if (BeardLength > 1f)
            BeardLength = BeardLength - BEARDGROWTHRATE;
        Debug.Log(vitality);
    }

	public void TakeDamage (int amount){
		if (MovementController.Shielding ())
			;//Take no damage if shielding
		else if (currentHealth <= 0) {
			currentHealth = 0; 
			gameObject.SetActive (false); 
			/* despawn need to add in some kind of animation with it */
		} else 
			currentHealth -= amount; 
	}
		

    private void FixedUpdate()
    {
        //Controls beard length
        if (Input.GetKey("e"))
            growBeard();
        if (Input.GetKey("q"))
            shrinkBeard();
    }
}
