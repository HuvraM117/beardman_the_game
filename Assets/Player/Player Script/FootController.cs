using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour {

    private Vector3 raycastOrigin = new Vector2(0f, -.9f);
    private RaycastHit2D downHit, rightHit, leftHit;
    private GameObject movingPlatform = null; // the moving platform the player is on if any

    private bool isFootGrounded;
    private bool isLeftCollided;
    private bool isRightCollided;

    public bool IsGrounded { get
        {
            // The original version -- doesn't work within the bounds of the flood
            // yes if triggered and EITHER raycast down is positive OR raycast to sides is negative
            //return triggeredTriggers > 0 && (downHit || (rightHit.collider == null && leftHit.collider == null));

            return triggeredTriggers > 0 && (isFootGrounded || (rightHit.collider == null && leftHit.collider == null));
            //return triggeredTriggers > 0 && (isFootGrounded || (isLeftCollided == false && isRightCollided == false));


            // Theoretically all we need
            //return isFootGrounded;

            //return ((triggeredTriggers > 0) && (isFootGrounded);
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
        downHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.down, .5f, LayerMask.GetMask("Ground"));
        rightHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.right, .5f, LayerMask.GetMask("Ground"));
        leftHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.left, .5f, LayerMask.GetMask("Ground"));

        isFootGrounded = Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(0, -1.0f, 0), 0.2f, LayerMask.GetMask("Ground"));
        isLeftCollided = Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(-0.75f, 0, 0), 0.1f, LayerMask.GetMask("Ground"));
        isRightCollided = Physics2D.OverlapCircle(gameObject.transform.position + new Vector3(0.75f, 0, 0), 0.1f, LayerMask.GetMask("Ground"));

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
