using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour {
	public GameObject Endoflevel;
	public GameObject Player;
	public int Range=10;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var scene = SceneManager.GetActiveScene();

        var levelComplete = LevelComplete();

        if (levelComplete && scene.name.Equals("LeadUpToBoss"))
        {
            SceneManager.LoadScene(3);
        }

        if (levelComplete)
        {
            SceneManager.LoadScene(0);
        }
        
	}

	bool LevelComplete(){
		return (Vector3.Distance (Player.transform.position, Endoflevel.transform.position) < Range);
	}

}
