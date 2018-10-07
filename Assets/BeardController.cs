using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardController : MonoBehaviour {

    public float followDistance = 0f;
    public bool isLimitedByDistance = true;
    public Rigidbody2D beardman;
    private MovementController movementController;

    public bool canGrapple = false;
    public bool isGrappling = false;
    public float grappleForce = 3f;
    private float grappleStrength = 0f;
    private Vector2 grapplePoint;
    Camera mainCamera;
	// Use this for initialization
	void Start () {
		if(beardman == null)
        {
            beardman = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;
        movementController = beardman.GetComponent<MovementController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isLimitedByDistance)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 followVector = (Vector2) mainCamera.ScreenToWorldPoint(mousePos) - beardman.position;
            this.transform.position = Vector2.ClampMagnitude(followVector, followDistance) + beardman.position;
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (followDistance < 3.75)
                followDistance += 0.25f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (followDistance > 0.25)
            followDistance -= 0.25f;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(canGrapple && grapplePoint != null)
            {
                Grapple();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "GrappleLatch")
        {
            canGrapple = true;
            grapplePoint = (Vector2) collision.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canGrapple = false;
    }

    private void Grapple()
    {
        isGrappling = true;
        if (grapplePoint.magnitude <= followDistance)
        {
            //Adds force in current direction 
            var dir = grapplePoint - beardman.position;

            beardman.AddForce(dir * grappleForce, ForceMode2D.Impulse);
            
        }
        isGrappling = false;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(beardman.position, this.transform.position);
    }

}
