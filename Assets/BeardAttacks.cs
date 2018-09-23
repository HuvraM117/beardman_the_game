using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardAttacks : MonoBehaviour {
	public GameObject particle;
	public GameObject character;
	public static Vector3 mousePosition;
	public float maxDis = 2f;

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
			//Placeholder for attacking
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
}

