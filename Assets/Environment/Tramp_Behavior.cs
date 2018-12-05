using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tramp_Behavior : MonoBehaviour {
	public Animator ani;

	// Use this for initialization
	void Start () {
		ani=gameObject.GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D col){

		ani.SetBool ("On",true);
		if (col.gameObject.name == "Beard Man"){



		}

	}
	void OnCollisionExit2D(Collision2D col){
		ani.SetBool ("On",false);

		if (col.gameObject.name == "Beard Man"){


			//ani.SetBool ("On",false);

		}

	}
}
