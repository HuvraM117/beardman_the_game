using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardAttacks : MonoBehaviour {
	public GameObject particle;
	public GameObject character;
	public static Vector3 mousePosition;

    [SerializeField] private PlayerState playerState;
    private BeardAnimationController beardAnimator;


    private void Awake()
    {
        beardAnimator = gameObject.GetComponentInChildren<BeardAnimationController>();
    }

    void FixedUpdate ()
	{
		//Uses mouse location for constant tracking of beard end location
		/*Vector3 old = particle.transform.position;
		Vector3 pos = Input.mousePosition;
		pos = Camera.main.ScreenToWorldPoint(pos) + new Vector3 (0, 0, 10);

		float distance = Vector3.Distance(character.transform.position, pos);
		if (distance > playerState.BeardLength)
			particle.transform.position = old;
		else
			particle.transform.position = pos;*/

		if (Input.GetMouseButtonDown(0))
		{
            UseBeard();
		}
	}

    public void UseBeard()
    {
        Vector2 targetPosition = particle.transform.position;
        RaycastHit2D targetHit = Physics2D.Raycast(targetPosition, Vector2.zero);
        GameObject targetObject = targetHit ? targetHit.collider.gameObject : null;

        // TODO: here I assume that all enemies/grappleable objects will have an associated component, we can change this later based on the actual components' names/different critereon
        if(targetObject && targetObject.GetComponent<GrapplePoint>() != null)
        {
            GrappleBeard(targetObject);
        }
        else
        {
            Debug.Log("LKSDFJHDSLKFJ");
            WhipBeard(targetPosition);
        }
    }

    // assuming the target is in range, not range-limited
    private void WhipBeard(Vector2 target)
    {
        beardAnimator.WhipBeard(target);
        Debug.Log("whip");
    }

    // assuming the target is in range, not range-limited
    private void GrappleBeard(GameObject grappleObject)
    {
        beardAnimator.GrappleBeard(grappleObject.transform);
        Debug.Log("grapple");
    }

}

