using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour {

    private Vector3 raycastOrigin = new Vector2(0f, -.9f);

    public bool IsGrounded { get
        {
            // yes if triggered and EITHER raycast down is positive OR raycast to sides is negative
            RaycastHit2D downHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.down, .5f);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.right, .5f);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position + raycastOrigin, Vector2.left, .5f);

            return triggeredTriggers > 0 && (downHit.collider != null || (rightHit.collider == null && leftHit.collider == null));
        }
    }

    private int triggerCount = 0;
    private int triggeredTriggers = 0;

    private void Start()
    {
        triggerCount = gameObject.GetComponents<Collider2D>().Length;
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
