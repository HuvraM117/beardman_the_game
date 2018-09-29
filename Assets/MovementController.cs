using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    private Rigidbody2D m_rigidbody;
    private Collider2D footCollider;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private const float MOVESPEED = 3f;
    private const float JUMPFORCE = 6f;

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
        else if (isGrounded)
        {
            moveInput.x = 0;
        }

        if(isGrounded && Input.GetButtonDown("Jump")) {
            moveInput.y = JUMPFORCE;
            isGrounded = false;
        }

        return moveInput;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
    }
}
