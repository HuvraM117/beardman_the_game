using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour {

    private Vector3 raycastOrigin = new Vector2(0f, -.9f);
    private RaycastHit2D downHit, rightHit, leftHit;
    private GameObject movingPlatform = null; // the moving platform the player is on if any
    private Vector3 previousPlatformPosition = Vector3.zero; // the position of the moving platform last frame (if on one)

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

        //update if on moving platform or not
        if(movingPlatform == null && downHit.collider != null && downHit.collider.gameObject.tag == "MovingPlatform")
        {
            movingPlatform = downHit.collider.gameObject;
            previousPlatformPosition = movingPlatform.transform.position;
        }
        else if(movingPlatform != null && (downHit.collider == null || downHit.collider.gameObject.tag != "MovingPlatform"))
        {
            movingPlatform = null;
        }
    }

    // we will need to update this if we want the player to be moved by other things, but for just moving platforms this should still work
    // NOTE: this resets the old platform position when called, so only call once per frame
    public Vector2 UpdatePlatformVelocity()
    {
        if(movingPlatform == null) { return Vector2.zero; }
        Debug.Log("NEW: " + movingPlatform.transform.position + " OLD: " + previousPlatformPosition + " DIFF " + (movingPlatform.transform.position - previousPlatformPosition));

        Vector2 platformVelocity = movingPlatform.transform.position - previousPlatformPosition;
        previousPlatformPosition = movingPlatform.transform.position;
        Debug.Log(movingPlatform);
        Debug.Log(platformVelocity);
        return platformVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggeredTriggers++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggeredTriggers--;
    }
}
