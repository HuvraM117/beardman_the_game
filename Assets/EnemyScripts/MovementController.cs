using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Rigidbody2D m_rigidbody;
    private Collider2D footCollider;
    [SerializeField] private FootController groundedState;
    private bool IsGrounded {
        get { return groundedState.IsGrounded; }
    }
    private bool isCrouching = false;
    [SerializeField] private float MOVESPEED = 5f;
    [SerializeField] private float JUMPFORCE = 12f;
	private Vector3 crouchSize;
	private Vector3 fullSize;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CircleCollider2D>();
		fullSize = new Vector3 (m_rigidbody.transform.localScale.x, 
			m_rigidbody.transform.localScale.y, m_rigidbody.transform.localScale.z);
		crouchSize = Vector3.Scale (fullSize, new Vector3 (1f, 0.5f, 1f));
	}
	
	// Update is called once per frame
	void Update () {
        isCrouching = Input.GetButton("Crouch");
        m_rigidbody.velocity = UpdateMovement();
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
			m_rigidbody.transform.localScale = fullSize;
        }
        else if (IsGrounded)
        {
			moveInput.x = Input.GetAxis("Horizontal") * 1.5f;
			m_rigidbody.transform.localScale = crouchSize;
        }

        if(IsGrounded && Input.GetButtonDown("Jump")) {
            moveInput.y = JUMPFORCE;
            // isGrounded = false;
        }

        return moveInput;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
    }
}
