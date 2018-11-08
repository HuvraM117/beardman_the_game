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
	public static bool pitchForkActive = false;
	[SerializeField] public GameObject leftFork;
	[SerializeField] public GameObject rightFork;
	public static Vector3 leftForkPos;
	public static Vector3 rightForkPos;

    public float grappleForce = 3f;
    private float grappleStrength = 0f;
    Camera mainCamera;
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
		leftFork.SetActive (false);
		rightFork.SetActive (false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLimitedByDistance)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 followVector = (Vector2)mainCamera.ScreenToWorldPoint(mousePos) - beardman.position;
            this.transform.position = Vector2.ClampMagnitude(followVector, playerState.BeardLength) + beardman.position;
			//this.transform.rotation = Quaternion.Euler (0, 0, 360 * (Mathf.Cos (this.transform.position.x - beardman.transform.position.x) 
				//+ Mathf.Sin (this.transform.position.y - beardman.transform.position.y)));
			this.transform.rotation = Quaternion.Euler (0, 0, Mathf.Atan2((this.transform.position.y - beardman.transform.position.y)
				, (this.transform.position.x - beardman.transform.position.x)) * 180 / Mathf.PI);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseBeard();
        }
		if (Input.GetKeyDown(KeyCode.P))
		{
			PitchFork();
		}
		leftForkPos = leftFork.transform.position;
		rightForkPos = rightFork.transform.position;
    }

    public void UseBeard()
    {
        Vector2 targetPosition = this.transform.position;
        RaycastHit2D targetHit = Physics2D.Raycast(targetPosition, Vector2.zero);
        GameObject targetObject = targetHit ? targetHit.collider.gameObject : null;

        // TODO: here I assume that all enemies/grappleable objects will have an associated component, we can change this later based on the actual components' names/different critereon
		if ((targetObject && targetObject.name == "Grapple Point") && !MovementController.Crouching())
        {
            GrappleBeard(targetObject);
        }
        else
        {
            WhipBeard(this.gameObject);
        }
    }


    // assuming the target is in range, not range-limited
    private void WhipBeard(GameObject targetObject)
    {
        beardAnimator.WhipBeard(targetObject.transform);
        Debug.Log("whip");
    }

    // assuming the target is in range, not range-limited
    private void GrappleBeard(GameObject grappleObject)
    {
        beardAnimator.WhipBeard(grappleObject.transform);
        var dir = (Vector2) grappleObject.transform.position- beardman.position;
		    beardman.velocity = Vector2.zero;
        
        beardman.AddForce(new Vector2(dir.x, 0) * grappleForce, ForceMode2D.Impulse);
        beardman.AddForce(dir * grappleForce, ForceMode2D.Impulse);
        Debug.Log("grapple");
    }

	private void PitchFork() {
		if (pitchForkActive) {
			leftFork.SetActive (false);
			rightFork.SetActive (false);
			pitchForkActive = false;
		} else {
			leftFork.SetActive (true);
			rightFork.SetActive (true);
			pitchForkActive = true;
		}
	}
}//end beard controller
