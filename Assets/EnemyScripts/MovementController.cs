using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Rigidbody2D m_rigidbody;
    private Collider2D footCollider;
	public Animator playerAnimator;
    [SerializeField] private FootController groundedState;
	[SerializeField] private GameObject shield;
    private bool IsGrounded {
        get { return groundedState.IsGrounded; }
    }
    private static bool isCrouching = false;
	private bool canShield = false;
	private static bool invincible = false;
	private bool lastDir = false;//False is left, true is right
    [SerializeField] private float MOVESPEED = 5f;
    [SerializeField] private float JUMPFORCE = 12f;
	private Vector3 crouchRight;
	private Vector3 fullSize;
	private Vector3 faceLeft;
	private Vector3 faceRight;
	private Vector3 crouchLeft;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CircleCollider2D>();
		fullSize = new Vector3 (m_rigidbody.transform.localScale.x, 
			m_rigidbody.transform.localScale.y, m_rigidbody.transform.localScale.z);
		crouchRight = Vector3.Scale (fullSize, new Vector3 (1f, 0.5f, 1f));
		crouchLeft = Vector3.Scale (fullSize, new Vector3 (-1f, 0.5f, 1f));
		faceLeft = Vector3.Scale (fullSize, new Vector3 (-1f, 1f, 1f));
		faceRight = Vector3.Scale (fullSize, new Vector3 (1f, 1f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
        isCrouching = Input.GetButton("Crouch");
        m_rigidbody.velocity = UpdateMovement();
		playerAnimator.SetFloat("Speed",m_rigidbody.velocity.magnitude);
		playerAnimator.SetBool ("Grounded", IsGrounded);

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
			moveInput.x = Input.GetAxis("Horizontal") * 1.5f;
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
			} else
				canShield = false;
        }

        if(IsGrounded && Input.GetButtonDown("Jump")) {
            moveInput.y = JUMPFORCE;
            // isGrounded = false;
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
			shield.SetActive (true);
			invincible = true;
		} else {
			shield.SetActive (false);
			invincible = false;
		}
	}

	public static bool Crouching() {
		return isCrouching;
	}

	public static bool Shielding() {
		return invincible;
	}
}
