using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardController : MonoBehaviour
{

    public bool isLimitedByDistance = true;
    public Rigidbody2D beardman;
    private MovementController movementController;
    private BeardAnimationController beardAnimator;
    [SerializeField] private PlayerState playerState;
	[SerializeField] private LineRenderer beardLine;
    [SerializeField] private Vector3 beardOriginOffset = Vector2.zero;

    public float grappleForce = 3f;
    private float grappleStrength = 0f;
    Camera mainCamera;
    private bool lsdaf = false;
    // Use this for initialization
    void Start()
    {
        if (beardman == null)
        {
            beardman = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;
        movementController = beardman.GetComponent<MovementController>();
        beardAnimator = beardman.GetComponentInChildren<BeardAnimationController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLimitedByDistance)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 followVector = (Vector2)mainCamera.ScreenToWorldPoint(mousePos) - beardman.position;
            this.transform.position = Vector2.ClampMagnitude(followVector, playerState.BeardLength) + beardman.position;
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2((this.transform.position.y - beardman.transform.position.y)
				, (this.transform.position.x - beardman.transform.position.x)) * 180 / Mathf.PI);
            beardLine.transform.position = beardman.transform.position + beardOriginOffset;

        }
        if (Input.GetKey(KeyCode.Mouse0))
            UseBeard();
    }


    public void UseBeard()
    {
        Vector2 targetPosition = this.transform.position;
        RaycastHit2D targetHit = Physics2D.Linecast(transform.position, beardman.position);
        GameObject targetObject = targetHit ? targetHit.collider.gameObject : null;

        // TODO: here I assume that all enemies/grappleable objects will have an associated component, we can change this later based on the actual components' names/different critereon
        if ((targetObject && targetObject.name == "Grapple Point") && !MovementController.Crouching())
        {
            GrappleBeard(targetHit.point);
        }
        else
        {
            WhipBeard(this.gameObject);
        }
    }


    // assuming the target is in range, not range-limited
    private void WhipBeard(GameObject targetObject)
    {
        beardAnimator.WhipBeard(targetObject.transform.position);
        Debug.Log("whip");
    }

    // assuming the target is in range, not range-limited
    private void GrappleBeard(Vector2 target)
    {

        beardAnimator.WhipBeard(target);
        var dir = (Vector2) target- beardman.position;
		beardman.velocity = new Vector2 (beardman.velocity.x, 0);
        
		beardman.AddForce(new Vector2(dir.x*2f, 0) * grappleForce , ForceMode2D.Impulse);
		beardman.AddForce(dir * grappleForce, ForceMode2D.Impulse);
        Debug.Log("grapple");
    }
}//end beard controller
