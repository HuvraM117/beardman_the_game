using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPopUp : MonoBehaviour {

    private SceneController sc;


    // Use this for initialization
    void Start() {

        if (!PlayerState.hasDied) { 
            sc = new SceneController();
            sc.PauseGame();
        }
        else
        {
            this.gameObject.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
