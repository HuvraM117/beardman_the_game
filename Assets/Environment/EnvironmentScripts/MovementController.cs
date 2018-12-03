using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Rigidbody2D m_rigidbody;
    private Collider2D footCollider;
	public Animator playerAnimator;
    private AudioSource footSource;
    private AudioSource musicSource;
    private AudioClip steps;
    private AudioClip sheildUp; //TODO
    private AudioClip sheildDown; //TODO

    [SerializeField] private FootController groundedState;
	[SerializeField] private GameObject shield;
    private bool IsGrounded {
        get { return groundedState.IsGrounded; }
    }
    private static bool isCrouching = false;
	private bool canShield = false;
	private static bool invincible = false;
	private Vector3 raycastOrigin = new Vector2(0f, 0.5f);
	private Vector3 raycastOrigin2 = new Vector2 (0f, 0.9f);
	private bool lastDir = false;//False is left, true is right
    [SerializeField] private float MOVESPEED = 5f;
    [SerializeField] private float JUMPFORCE = 12f;
	private Vector3 crouchRight;
	private Vector3 fullSize;
	private Vector3 faceLeft;
	private Vector3 faceRight;
	private Vector3 crouchLeft;

    private bool playedSheildUpSound = false;
    private bool playedSheildDownSound = true;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CircleCollider2D>();
		fullSize = new Vector3 (m_rigidbody.transform.localScale.x, 
			m_rigidbody.transform.localScale.y, m_rigidbody.transform.localScale.z);
		crouchRight = Vector3.Scale (fullSize, new Vector3 (1f, 1f, 1f));
		crouchLeft = Vector3.Scale (fullSize, new Vector3 (-1f, 1f, 1f));
		faceLeft = Vector3.Scale (fullSize, new Vector3 (-1f, 1f, 1f));
		faceRight = Vector3.Scale (fullSize, new Vector3 (1f, 1f, 1f));

        //Audio Things
        var beardman = GameObject.Find("Beard Man/FootAudioSource");

        footSource = beardman.GetComponents<AudioSource>()[0];

        steps = Resources.LoadAll<AudioClip>("Sound/Steps")[0];

        AudioClip[] beardManSounds = Resources.LoadAll<AudioClip>("Sound/BeardManSounds");

        footSource.clip = steps;

        var beardmanMusic = GameObject.Find("Beard Man/MusicMaker");

        musicSource = beardmanMusic.GetComponents<AudioSource>()[0];

        sheildUp = beardManSounds[2];
        sheildDown = beardManSounds[3];
	}
	
	// Update is called once per frame
	void Update () {
        if (isCrouching && (Physics2D.Raycast(m_rigidbody.transform.position + raycastOrigin2, Vector2.left, 0.5f).collider != null
            || Physics2D.Raycast(m_rigidbody.transform.position + raycastOrigin2, Vector2.right, 0.5f).collider != null
            || Physics2D.Raycast(m_rigidbody.transform.position + raycastOrigin, Vector2.up, 1f).collider != null))
        {
            isCrouching = true;
        }
        else
            isCrouching = Input.GetButton("Crouch");

        if(isCrouching && !playedSheildUpSound)
        {
            //Debug.Log("play shield up");
            musicSource.PlayOneShot(sheildUp);
            playedSheildUpSound = true;
            playedSheildDownSound = false;
        }

        if(!isCrouching && !playedSheildDownSound)
        {
            //Debug.Log("play shield down");
            musicSource.PlayOneShot(sheildDown);
            playedSheildDownSound = true;
            playedSheildUpSound = false;
        }

        m_rigidbody.velocity = UpdateMovement();
		playerAnimator.SetFloat("Speed", System.Math.Abs(m_rigidbody.velocity.x));
        
		playerAnimator.SetBool ("Grounded", IsGrounded);

        if( ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && IsGrounded)
        {
            //Debug.Log(footSource.isPlaying);
            if (!footSource.isPlaying)
            {
                footSource.Play();
            }
            
        }
        else
        {
            footSource.Stop();
        }

    }

    private Vector2 UpdateMovement()
    {
        Vector2 moveInput = m_rigidbody.velocity;

        if(PlayerState.CurrentBeardState == BeardState.PULLING)
        {
            return moveInput;
        }

        if(!isCrouching)
        {
            moveInput.x = Input.GetAxis("Horizontal") * MOVESPEED;
			canShield = false;
			if (moveInput.x > 0) {
				m_rigidbody.transform.localScale = faceRight;
				lastDir = true;
			} else if (moveInput.x < 0) {
				m_rigidbody.transform.localScale = faceLeft;
				lastDir = false;
			} else {
				if (lastDir)
					m_rigidbody.transform.localScale = faceRight;
				else
					m_rigidbody.transform.localScale = faceLeft;
			}
        }
        else if (IsGrounded)
        {
            
			moveInput.x = Input.GetAxis("Horizontal") * MOVESPEED;
			if (moveInput.x > 0) {
				m_rigidbody.transform.localScale = crouchRight;
				lastDir = true;
				canShield = false;
			} else if (moveInput.x < 0) {
                m_rigidbody.transform.localScale = crouchLeft;
                lastDir = false;
				canShield = false;
			} else if (moveInput.x == 0) {
				canShield = true;
                if (lastDir)
                    m_rigidbody.transform.localScale = crouchRight;
                else
                    m_rigidbody.transform.localScale = crouchLeft;
            }
            else
				canShield = false;
        }
        if(IsGrounded && Input.GetButtonDown("Jump"))
        {
            playerAnimator.SetBool("Grounded", false);
            playerAnimator.SetTrigger("Jump");
            moveInput.y = JUMPFORCE;
        }
		PlayerCrouch ();

        return moveInput;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
    }

	private void PlayerCrouch() {
		if (canShield && IsGrounded) {
			//shield.SetActive (true);
			invincible = true;
            playerAnimator.SetBool("Shielded", true);
        } else {
			//shield.SetActive (false);
			invincible = false;
            playerAnimator.SetBool("Shielded", false);
        }

        playerAnimator.SetBool("Crouching", isCrouching);
    }

	public static bool Crouching() {
		return isCrouching;
	}

	public static bool Shielding() {
		return invincible;
	}
}
