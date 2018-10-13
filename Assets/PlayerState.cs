using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeardState { IDLE, EXTENDING, RETRACTING, PULLING };

// a state machine for the player
// putting it in it's own class encapsulates and extracts the underlying state nicely
public class PlayerState : MonoBehaviour {

    [SerializeField] private static float MAXVITALITY = 7f;
    [SerializeField] private SliderState healthBeardUI;

    private static BeardState _currentBeardState = BeardState.IDLE;
    public static BeardState CurrentBeardState { get; set; }

    // setting up health and beard length as properties this way automatically ensures that increasing one decreases the other and vice versa
    private float health;
    public float Health {
        get { return health; }
        private set {
            beardLength += health - value;
            health = value;
            healthBeardUI.UpdateSlider(health, beardLength);
        }
    }

    private float beardLength;
    public float BeardLength {
        get { return beardLength; }
        private set
        {
            health += beardLength - value;
            beardLength = value;
            healthBeardUI.UpdateSlider(health, beardLength);
        }
    }

    private const float BEARDGROWTHRATE = .02f;


    private void Start()
    {
        health = MAXVITALITY / 2;
        beardLength = MAXVITALITY / 2;
        healthBeardUI.UpdateSlider(health, beardLength);
    }

    // beard length + health, the total resource, I can't think of what else to call it so hopefully someone else can
    public float vitality
    {
        get
        {
            return beardLength + health;
        }
    }

    //Increases the maximum attack range
    public void growBeard()
    {
        if (health - BEARDGROWTHRATE > 0)
            BeardLength = BeardLength + BEARDGROWTHRATE;
    }

    //Shrinks the maximum attack range
    public void shrinkBeard()
    {
        if (BeardLength > 1f)
            BeardLength = BeardLength - BEARDGROWTHRATE;
    }

	public void TakeDamage (int amount){
		if (health <= 0){
			health = 0; 
			gameObject.SetActive(false); 
			/* despawn need to add in some kind of animation with it */
		}
		health -= amount;
        healthBeardUI.UpdateSlider(health, beardLength);
	}
		

    private void FixedUpdate()
    {
        //Controls beard length
        if (Input.GetKey("q"))
            growBeard();
        if (Input.GetKey("e"))
            shrinkBeard();
    }
}
