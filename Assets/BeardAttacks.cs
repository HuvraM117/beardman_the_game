﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardAttacks : MonoBehaviour {
	public GameObject particle;
	public GameObject character;
	public static Vector3 mousePosition;
	public static float maxDis = 2f;

    private BeardAnimationController beardAnimator;


    private void Awake()
    {
        beardAnimator = gameObject.GetComponentInChildren<BeardAnimationController>();
    }

    void FixedUpdate ()
	{
		//Uses mouse location for constant tracking of beard end location
		Vector3 old = particle.transform.position;
		Vector3 pos = Input.mousePosition;
		pos = Camera.main.ScreenToWorldPoint(pos) + new Vector3 (0, 0, 9);

		float distance = Vector3.Distance(character.transform.position, pos);
		if (distance > maxDis)
			particle.transform.position = old;
		else
			particle.transform.position = pos;

		if (Input.GetMouseButtonDown(0))
		{
            UseBeard();
		}
		//Controls beard length
		if (Input.GetKey ("e"))
			growBeard ();
		if (Input.GetKey ("q"))
			shrinkBeard ();
	}

	//Increases the maximum attack range
	public void growBeard() {
		if (maxDis < 3f)
			maxDis = maxDis + 0.02f;
	}

	//Shrinks the maximum attack range
	public void shrinkBeard() {
		if (maxDis > 1f)
			maxDis = maxDis - 0.02f;
	}

	//Returns current beard size
	public static float getBeard() {
		return maxDis;
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
            WhipBeard(particle);
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
        beardAnimator.GrappleBeard(grappleObject.transform);
        Debug.Log("grapple");
    }

}
