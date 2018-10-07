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

    //The input values calculated from update every frame.
    private float horizontal;
    private bool jump;

    //Field which indicates whether or not the character is on the ground. If set, the player can jump.
    private bool grounded;

    Vector2 velocity;

    private BeardController beardController;
	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CircleCollider2D>();
        beardController = GameObject.Find("Beard").GetComponent<BeardController>();
	}
    
    void FixedUpdate()
    {
        isCrouching = Input.GetButton("Crouch");
        UpdateMovement();
    }

    // TODO: fix bug where it sticks to walls
    private void UpdateMovement()
    {
        Vector2 moveInput = m_rigidbody.velocity;

        if (!isCrouching)
        {
            moveInput.x = Input.GetAxis("Horizontal") * MOVESPEED;
        }
        else if (isGrounded)
        {
            moveInput.x = 0;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveInput.y = JUMPFORCE;
            isGrounded = false;
        }

        if (!beardController.isGrappling)
            m_rigidbody.velocity = moveInput;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Floor")
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
    }

    public void setGrounded(bool isGround)
    {
        isGrounded = isGround;
    }
}
