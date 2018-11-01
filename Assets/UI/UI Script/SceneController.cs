using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour {
	public GameObject Endoflevel;
	public GameObject Player;
    private Camera BossLevelCamera;
    private Camera normalCamera;

	public int Range=10;
	// Use this for initialization
	void Start () {
        //BossLevelCamera = this.gameObject.GetComponents<Camera>()[0];
        //normalCamera = Player.GetComponents<Camera>()[0];

	}
	
	// Update is called once per frame
	void Update () {
        var sceneName = SceneManager.GetActiveScene().name;

        var levelComplete = LevelComplete();

        if (sceneName.Equals("LeadUpToBoss"))
        { 
            if (LevelComplete())
            {
                SceneManager.LoadScene(3);
            }
        }else if (LevelComplete())
        {
            SceneManager.LoadScene(0);
        }
        
	}

	bool LevelComplete(){

        return (Vector3.Distance (Player.transform.position, Endoflevel.transform.position) < Range);
        
    }

    
}
