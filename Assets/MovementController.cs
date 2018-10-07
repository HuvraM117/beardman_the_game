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

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        isCrouching = Input.GetButton("Crouch");
        m_rigidbody.velocity = UpdateMovement();
	}

    // TODO: fix bug where it sticks to walls
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
        }
        else if (IsGrounded)
        {
            moveInput.x = 0;
        }

        if(IsGrounded && Input.GetButtonDown("Jump")) {
            moveInput.y = JUMPFORCE;
            // isGrounded = false;
        }

        return moveInput;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
    }
}
