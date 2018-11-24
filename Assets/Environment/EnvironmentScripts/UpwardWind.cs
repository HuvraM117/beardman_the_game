using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardWind : MonoBehaviour {
	public Rigidbody2D beardman;
	public float windforce=3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay2D(Collider2D col){


		if (col.gameObject.name == "Feet" && !Input.GetKey(KeyCode.W)){
			new WaitForSeconds (3f);
			beardman.velocity =new Vector2 (beardman.velocity.x,-windforce);

		}

		if (col.gameObject.name == "Feet" && Input.GetKeyDown(KeyCode.W)){

			new WaitForSeconds (3f);

		}


	}
}
