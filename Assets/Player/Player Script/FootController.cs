using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour {

    private Vector3 raycastOrigin = new Vector2(0f, -.9f);
    private RaycastHit2D downHit, rightHit, leftHit;
    private GameObject movingPlatform = null; // the moving platform the player is on if any

    public bool IsGrounded { get
        {
            // yes if triggered and EITHER raycast down is positive OR raycast to sides is negative
            return triggeredTriggers > 0 && (downHit.collider != null || (rightHit.collider == null && leftHit.collider == null));
        }
    }

    private int triggerCount = 0;
    private int triggeredTriggers = 0;

    private void Start()
    {
        triggerCount = gameObject.GetComponents<Collider2D>().Length;
    }

    // update hits only once every physics update
    private void FixedUpdate()
    {
        //update raycasts
        downHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.down, .5f);
        rightHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.right, .5f);
        leftHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.left, .5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name != "Grapple Point")
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
