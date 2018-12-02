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
	private Vector3 beardAimPoint;

	public static bool retracting = false;
	public static bool grappleLate = false;
	public static RaycastHit2D targetLate;
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
			this.transform.position = Vector2.ClampMagnitude(followVector, playerState.BeardLength + 1) + beardman.position;
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2((beardAimPoint.y - beardman.transform.position.y)
				, (beardAimPoint.x - beardman.transform.position.x)) * 180 / Mathf.PI);
			beardLine.transform.position = beardman.transform.position + beardOriginOffset;

		}
		if (Input.GetKey (KeyCode.Mouse0))
			UseBeard();
		
		if (retracting) {
			GrappleBeard(targetLate.point);
			grappleLate = false;
			retracting = false;
		}
	}


	public void UseBeard()
	{
		Vector2 targetPosition = this.transform.position;
		RaycastHit2D targetHit = Physics2D.Linecast(transform.position, beardman.position);
		GameObject targetObject = targetHit ? targetHit.collider.gameObject : null;

		// TODO: here I assume that all enemies/grappleable objects will have an associated component, we can change this later based on the actual components' names/different critereon
		if ((targetObject && targetObject.name == "Grapple Point") && !MovementController.Crouching())
		{
			beardAimPoint = this.transform.position;
			grappleLate = true;
			targetLate = targetHit;
			beardAnimator.WhipBeard(targetHit.point);
		}
		else
		{
			beardAimPoint = this.transform.position;
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
		
		var dir = (Vector2) target- beardman.position;
		beardman.velocity = new Vector2 (beardman.velocity.x, 0);

		beardman.AddForce(new Vector2(dir.x*2f, 0) * grappleForce , ForceMode2D.Impulse);
		beardman.AddForce(dir * grappleForce, ForceMode2D.Impulse);
		if (beardman.velocity.x > 25f)
			beardman.velocity = new Vector2(25f, beardman.velocity.y);
		else if (beardman.velocity.x < 10f && beardman.velocity.x > 0)
			beardman.velocity = new Vector2(10f, beardman.velocity.y);
		if (beardman.velocity.y > 25f)
			beardman.velocity = new Vector2(beardman.velocity.x, 25f);
		else if (beardman.velocity.y < 10f && beardman.velocity.y > 0)
			beardman.velocity = new Vector2(beardman.velocity.x, 10f);
		Debug.Log("grapple");
	}
}//end beard controller