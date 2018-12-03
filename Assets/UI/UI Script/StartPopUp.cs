using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPopUp : MonoBehaviour {

    private SceneController sc;

	// Use this for initialization
	void Start () {

        sc = new SceneController();
        sc.PauseGame();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
