using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{

    private Vector3 raycastOrigin = new Vector2(0f, -.9f);
    private RaycastHit2D downHit, rightHit, leftHit;
    private GameObject movingPlatform = null; // the moving platform the player is on if any

    private bool isCurrentlyGrounded;

    public bool IsGrounded
    {
        get
        {
            // yes if triggered and EITHER raycast down is positive OR raycast to sides is negative
            //TODO: Difficulties w/ flood collider make raycast method difficult
            ////return (triggeredTriggers > 0) && (downHit.collider != null || (rightHit.collider == null && leftHit.collider == null));
            return isCurrentlyGrounded;
        }
    }

    private int triggerCount = 0;
    private int triggeredTriggers = 0;

    private void Start()
    {
        triggerCount = gameObject.GetComponents<Collider2D>().Length;

        //Physics.IgnoreCollision(floodObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    // update hits only once every physics update
    private void FixedUpdate()
    {
        // Checks a position below the player's current one, checks for collision 
        isCurrentlyGrounded = Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(0, -1.0f, 0), 0.2f, LayerMask.GetMask("GroundLayer"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Grapple Point")
            triggeredTriggers++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Grapple Point")
            triggeredTriggers--;
    }

    public void ManuallyDecrementTriggers()
    {
        triggeredTriggers--;
    }
}
