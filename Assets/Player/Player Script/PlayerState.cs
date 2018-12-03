using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BeardState { IDLE, EXTENDING, RETRACTING, PULLING };

// a state machine for the player
// putting it in it's own class encapsulates and extracts the underlying state nicely
public class PlayerState : MonoBehaviour
{

    [SerializeField] private static int INITIALVITALITY = 6;
    private static int MAXIMUMVITALITY = 10;
    
    //[SerializeField] private SliderState healthBeardUI;
    private SliderState healthBeardUI;

    private AudioClip beardGrow;
    private AudioClip beardShrink;
    private AudioClip beardManDeath;
    private AudioClip beardManHurt;
	public GameObject death_UI;
    private static BeardState _currentBeardState = BeardState.IDLE;
    public static BeardState CurrentBeardState { get; set; }
    private AudioSource musicSource;
    private AudioSource backgroundSource;
    private float beardKeyPressTime = 0f;

    public static bool hasDied;

    private Animator animator;
    // setting up health and beard length as properties this way automatically ensures that increasing one decreases the other and vice versa
    private int health;
    public int Health
    {
        get { return health; }
        private set
        {
            beardLength += health - value;
            health = value;
            healthBeardUI.UpdateSlider(health, beardLength);
        }
    }

    private int beardLength;
    public int BeardLength
    {
        get { return beardLength; }
        private set
        {
            health += beardLength - value;
            beardLength = value;
            healthBeardUI.UpdateSlider(health, beardLength);
        }
    }

    private const int BEARDGROWTHRATE = 1;


    private void Start()
    {
        health = INITIALVITALITY / 2;
        beardLength = INITIALVITALITY / 2;

        //getting health ui
        var slider = GameObject.Find("Canvas/HealthBar");
        healthBeardUI = slider.GetComponent<SliderState>();

        healthBeardUI.InitializeSegments();
        healthBeardUI.UpdateSlider(health, beardLength);

        animator = transform.GetComponentInChildren<Animator>();
        animator.SetFloat("Health", health);

        //Audio things 
        var beardman = GameObject.Find("Beard Man/MusicMaker");

        var b2 = GameObject.Find("Beard Man/BackgroundMusicSource");

        musicSource = beardman.GetComponents<AudioSource>()[0];
        backgroundSource = b2.GetComponents<AudioSource>()[0];


        AudioClip[] beardSounds = Resources.LoadAll<AudioClip>("Sound/BeardNoise");
        AudioClip[] beardManSounds = Resources.LoadAll<AudioClip>("Sound/BeardManSounds");

        beardGrow = beardSounds[4];
        beardShrink = beardSounds[3];

        beardManDeath = beardManSounds[1];
        beardManHurt = beardManSounds[0];

    }

    // beard length + health, the total resource, I can't think of what else to call it so hopefully someone else can
    public int vitality
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
        {
            musicSource.PlayOneShot(beardGrow);
            BeardLength = BeardLength + BEARDGROWTHRATE;
        }  
    }

    //Shrinks the maximum attack range
    public void shrinkBeard()
    {
        if (BeardLength > 1)
        {
            musicSource.PlayOneShot(beardShrink);
            BeardLength = BeardLength - BEARDGROWTHRATE;
        }  
    }

    public void TakeDamage(int amount)
    {
        animator.SetTrigger("Damage");
        if (MovementController.Shielding() && amount < 1000)
            ;//Take no damage if shielding
        else if (health - amount <= 0)
        {
            health = 0;
            //gameObject.SetActive (false);
            StartCoroutine(playerDie());
            /* despawn need to add in some kind of animation with it */
        }
        else
        {
            health -= amount;
            if(amount > 0)
                musicSource.PlayOneShot(beardManHurt);
        }
           
        animator.SetFloat("Health", health);
        // the only time vitality increases is here, so cap it at the max vitality
        if(vitality > MAXIMUMVITALITY)
        {
            Debug.Log(vitality);
            // if we can take the extra off beard length, do it, otherwise just undo the additional health
            if(beardLength > 1)
            {
                beardLength += MAXIMUMVITALITY - vitality;
                Debug.Log("capping beard length");
            }
            else
            {
                health += MAXIMUMVITALITY - vitality;
                Debug.Log("capping health");
            }
        }
        healthBeardUI.UpdateSlider(health, beardLength);

    }

    IEnumerator playerDie()
    {
        gameObject.GetComponent<MovementController>().enabled = false;
        animator.SetFloat("Health", health);
		death_UI.SetActive (true);
        backgroundSource.Stop();
        musicSource.PlayOneShot(beardManDeath);
        hasDied = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void Update()
    {
        //Controls beard length
        if (Input.GetKeyDown("q")) 
            growBeard();
        if (Input.GetKeyDown("e"))
            shrinkBeard();
        //if (Input.GetKeyDown(KeyCode.F10))
        //    TakeDamage(1);
        //if (Input.GetKeyDown(KeyCode.F11))
        //    TakeDamage(-1);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("q"))
            beardKeyPressTime += .2f;
        else if (Input.GetKey("e"))
            beardKeyPressTime -= .2f;
        else
            beardKeyPressTime = 0f;
        if (beardKeyPressTime > 1f)
        {
            growBeard();
            beardKeyPressTime = 0f;
        }
        else if (beardKeyPressTime < -1f)
        {
            shrinkBeard();
            beardKeyPressTime = 0f;
        }
    }
}
