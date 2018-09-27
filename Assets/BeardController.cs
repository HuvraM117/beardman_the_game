using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardController : MonoBehaviour {

    public float followDistance = 0f;
    public bool isLimitedByDistance = true;
    public Rigidbody2D beardman;

    public bool isGrappling = false;
    Camera mainCamera;
	// Use this for initialization
	void Start () {
		if(beardman == null)
        {
            beardman = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "GrappleLatch")
        {
            isGrappling = true;
            Vector2 distance = (Vector2) collision.transform.position - beardman.position;
            if(distance.magnitude <= followDistance)
                Grapple(distance);
        }
    }

    private void Grapple(Vector2 distance)
    {
        //Adds force in current direction 
        var dir = distance;
        float force = 3f;

        Debug.Log(distance * force);

        beardman.AddForce(dir * force, ForceMode2D.Impulse);
        //if (distance.x > 0)
        //    beardman.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        //else if (distance.x < 0)
        //    beardman.AddForce(Vector2.left * force, ForceMode2D.Impulse);

        isGrappling = false;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(beardman.position, this.transform.position);
    }

}
