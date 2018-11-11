using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObjects : MonoBehaviour {

    [SerializeField] private GameObject beardTip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

/*	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.name.Contains("Bear")){
			Destroy(gameObject);
		}
		//Debug.Log("Touchedddddddd");

	}
	*/
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.name.Contains("test")){
			Debug.Log("Touchedddddddd");
			Destroy(gameObject);

		}

        if(col.gameObject == beardTip)
        {
            Destroy(gameObject);
        }


	}
		
		
}
