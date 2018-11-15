using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BeardState { IDLE, EXTENDING, RETRACTING, PULLING };

// a state machine for the player
// putting it in it's own class encapsulates and extracts the underlying state nicely
public class PlayerState : MonoBehaviour
{

    [SerializeField] private static float MAXVITALITY = 7f;
    [SerializeField] private SliderState healthBeardUI;

    private static BeardState _currentBeardState = BeardState.IDLE;
    public static BeardState CurrentBeardState { get; set; }

    private Animator animator;
    // setting up health and beard length as properties this way automatically ensures that increasing one decreases the other and vice versa
    private float health;
    public float Health
    {
        get { return health; }
        private set
        {
            beardLength += health - value;
            health = value;
            healthBeardUI.UpdateSlider(health, beardLength);
        }
    }

    private float beardLength;
    public float BeardLength
    {
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

        animator = transform.GetComponentInChildren<Animator>();
        animator.SetFloat("Health", health);
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

    public void TakeDamage(int amount)
    {
        if (MovementController.Shielding())
            ;//Take no damage if shielding
        else if (health - amount <= 0)
        {
            health = 0;
            //gameObject.SetActive (false);
            StartCoroutine(playerDie());
            /* despawn need to add in some kind of animation with it */
        }
        else
            health -= amount;
        healthBeardUI.UpdateSlider(health, beardLength);
        animator.SetFloat("Health", health);
    }

    IEnumerator playerDie()
    {
        gameObject.GetComponent<MovementController>().enabled = false;
        animator.SetFloat("Health", health);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void FixedUpdate()
    {
        //Controls beard length
        if (Input.GetKey("q")) //TODO add the grow/shrink sounds here
            growBeard();
        if (Input.GetKey("e"))
            shrinkBeard();
        if (Input.GetKey(KeyCode.F10))
            TakeDamage(10);
    }
}
